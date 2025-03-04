namespace BE_RestaurantManagement.DTOs
{
    public class OrderGetAllRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int? KitchenStaffId { get; set; }
        public string? KitchenStaffName { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; }

    }
}
