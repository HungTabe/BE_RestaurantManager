namespace BE_RestaurantManagement.DTOs
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public string? MenuItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
