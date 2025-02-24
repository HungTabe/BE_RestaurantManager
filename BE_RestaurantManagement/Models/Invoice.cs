using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public int PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
