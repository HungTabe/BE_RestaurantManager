using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.DTOs
{
    public class OrderItemRequest
    {
        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
