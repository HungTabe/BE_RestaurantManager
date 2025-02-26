namespace BE_RestaurantManagement.Models
{
    public class KitchenStaff : User
    {

        // List of orders that kitchen staff are processing
        public ICollection<Order> AssignedOrders { get; set; } = new List<Order>();

    }
}
