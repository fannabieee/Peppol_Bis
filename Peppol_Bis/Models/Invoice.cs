namespace Peppol_Bis.Models
{
    public class Invoice
    {
        public string? InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Currency { get; set; }
    }
}
