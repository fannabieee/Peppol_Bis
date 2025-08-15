using Microsoft.AspNetCore.Mvc;
using Peppol_Bis.Dtos.Request;
using Peppol_Bis.Models;
using System.Globalization;
using System.Xml.Linq;

namespace Peppol_Bis.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController(PeppolUblContext context) : ControllerBase
    {
        private readonly PeppolUblContext _context = context;

        [HttpPost("import-xml")]
        public async Task<IActionResult> ImportInvoiceXml([FromBody] InvoiceRequest invoiceRequest)
        {
            try
            {
                XDocument doc = XDocument.Parse(invoiceRequest.XmlString);

                XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
                XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";

                string invoiceNumber = doc.Descendants(cbc + "ID").FirstOrDefault()?.Value ?? string.Empty;
                string issueDateStr = doc.Descendants(cbc + "IssueDate").FirstOrDefault()?.Value ?? string.Empty;
                DateOnly issueDate = DateOnly.FromDateTime(DateTime.Parse(issueDateStr));

                string supplierName = doc.Descendants(cac + "AccountingSupplierParty")
                                        .Descendants(cac + "PartyName")
                                        .Descendants(cbc + "Name").FirstOrDefault()?.Value ?? string.Empty;

                string supplierAddress = doc.Descendants(cac + "AccountingSupplierParty")
                                            .Descendants(cac + "PostalAddress")
                                            .Descendants(cbc + "StreetName").FirstOrDefault()?.Value ?? string.Empty;

                string customerName = doc.Descendants(cac + "AccountingCustomerParty")
                                        .Descendants(cac + "PartyName")
                                        .Descendants(cbc + "Name").FirstOrDefault()?.Value ?? string.Empty;

                string customerAddress = doc.Descendants(cac + "AccountingCustomerParty")
                                            .Descendants(cac + "PostalAddress")
                                            .Descendants(cbc + "StreetName").FirstOrDefault()?.Value ?? string.Empty;

                var totalAmountElem = doc.Descendants(cac + "LegalMonetaryTotal")
                                         .Descendants(cbc + "PayableAmount").FirstOrDefault();

                decimal totalAmount = 0;
                string currency = string.Empty;

                if (totalAmountElem != null)
                {
                    totalAmount = decimal.Parse(totalAmountElem.Value, CultureInfo.InvariantCulture);
                    currency = totalAmountElem.Attribute("currencyID")?.Value ?? currency;
                }

                // Tạo đối tượng Invoice
                var invoice = new Invoice
                {
                    InvoiceNumber = invoiceNumber,
                    IssueDate = issueDate,
                    SupplierName = supplierName,
                    SupplierAddress = supplierAddress,
                    CustomerName = customerName,
                    CustomerAddress = customerAddress,
                    TotalAmount = totalAmount,
                    Currency = currency
                };

                await _context.Invoices.AddAsync(invoice);
                await _context.SaveChangesAsync();

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
