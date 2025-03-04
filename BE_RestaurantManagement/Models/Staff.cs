namespace BE_RestaurantManagement.Models
{
    public class Staff : User
    {
        public ICollection<Order> ProcessedOrders { get; set; } = new List<Order>();
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
