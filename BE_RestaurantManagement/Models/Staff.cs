namespace BE_RestaurantManagement.Models
{
    public class Staff : User
    {
        public ICollection<Order> ProcessedOrders { get; set; }
        public ICollection<Shift> Shifts { get; set; }
    }
}
