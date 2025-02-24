using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int TableId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public DateTime ReservationTime { get; set; }
    }
}
