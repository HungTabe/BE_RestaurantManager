using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Content { get; set; }

        public int Rating { get; set; } // 1 - 5 stars
    }
}
