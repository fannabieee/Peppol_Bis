using Microsoft.AspNetCore.Mvc;
using Peppol_Bis.Models;
using System.Globalization;
using System.Xml.Linq;

namespace Peppol_Bis.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpPost("import-xml")]
        public IActionResult ImportInvoiceXml([FromBody] InvoiceRequest invoiceRequest)
        {
            try
            {
                XDocument doc = XDocument.Parse(invoiceRequest.XmlString);

                XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
                XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";

                string invoiceNumber = doc.Descendants(cbc + "ID").FirstOrDefault()?.Value ?? string.Empty;
                string issueDateStr = doc.Descendants(cbc + "IssueDate").FirstOrDefault()?.Value ?? string.Empty;
                DateTime issueDate = DateTime.Parse(issueDateStr);

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

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
