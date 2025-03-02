using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PaymentMethod { get; set; } // Cash, ZaloPay

        [Column(TypeName = "nvarchar(50)")]
        public string PaymentStatus { get; set; } // Pending, Processing, Completed
    }
}
