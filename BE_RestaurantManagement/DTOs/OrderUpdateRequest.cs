using BE_RestaurantManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.DTOs
{
    public class OrderUpdateRequest
    {
        public int CustomerId { get; set; }

        public List<OrderItemRequest> OrderItems { get; set; }

        public int KitchenStaffId { get; set; }

        public int StaffId { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }




    }
}
