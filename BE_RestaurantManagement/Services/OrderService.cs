using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BE_RestaurantManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OrderService(RestaurantDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var kitchenStaff = await _context.KitchenStaffs.FindAsync(request.KitchenStaffId);
            if (customer == null)
            {
                throw new Exception("KitchenStaff not found");
            }

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Invalid token: User ID not found");
            }
            int userId = int.Parse(userIdClaim.Value);

            var order = new Order
            {
                CustomerId = request.CustomerId,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>(),
                StaffId = userId,
                KitchenStaffId = request.KitchenStaffId
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
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Staff)
            .Include(o => o.KitchenStaff)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return order ?? throw new KeyNotFoundException("Order not found.");
        }
    }
}
