using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.DTOs
{
    public class OrderCreateRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<OrderItemRequest> OrderItems { get; set; }

        [Required]
        public int KitchenStaffId { get; set; }
    }
}
