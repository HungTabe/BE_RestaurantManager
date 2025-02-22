using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext _context;

        public OrderService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var order = new Order
            {
                CustomerId = request.CustomerId,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in request.OrderItems)
            {
                var menuItem = await _context.MenuItems.FindAsync(item.MenuItemId);
                if (menuItem == null || !menuItem.IsAvailable)
                {
                    throw new Exception($"Menu item {item.MenuItemId} is not available");
                }

                var orderItem = new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    TotalPrice = menuItem.Price * item.Quantity
                };

                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
