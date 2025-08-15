using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peppol_Bis.Models;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? InvoiceNumber { get; set; }

    public DateOnly? IssueDate { get; set; }

    [StringLength(255)]
    public string? SupplierName { get; set; }

    [StringLength(255)]
    public string? SupplierAddress { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? CustomerAddress { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [StringLength(10)]
    public string? Currency { get; set; }
}
