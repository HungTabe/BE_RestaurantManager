using Azure.Core;
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

            var kitchenStaff = await _context.Users
                .Where(u => u.RoleId == 6 && u.UserId == request.KitchenStaffId)
                .FirstOrDefaultAsync();

            if (kitchenStaff == null)
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
                KitchenStaffId = kitchenStaff.UserId
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

        public async Task<IEnumerable<OrderGetAllRequest>> GetAllOrdersAsync()
        {

            return await _context.Orders
                 .Select(o => new OrderGetAllRequest
                 {
                     OrderId = o.OrderId,
                     CustomerId = o.CustomerId,
                     CustomerName = o.Customer.FullName,
                     StaffId = o.Staff.UserId,
                     StaffName = o.Staff.FullName,
                     KitchenStaffId = o.KitchenStaffId,
                     KitchenStaffName = o.KitchenStaff.FullName,
                     OrderDate = o.OrderDate,
                     OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                     {
                         OrderItemId = oi.OrderItemId,
                         MenuItemId = oi.MenuItemId,
                         MenuItemName = oi.MenuItem.Name,
                         Price = oi.MenuItem.Price,
                         Quantity = oi.Quantity,
                         TotalPrice = oi.TotalPrice
                     }).ToList()
                 })
                 .ToListAsync();
        }

        public async Task<OrderGetAllRequest> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .Where(o => o.OrderId == orderId)
            .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Truy vấn lấy tên Staff và KitchenStaff từ Users
            var staffName = await _context.Users
                .Where(u => u.UserId == order.StaffId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            var kitchenStaffName = await _context.Users
                .Where(u => u.UserId == order.KitchenStaffId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            return new OrderGetAllRequest
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer.FullName,
                StaffId = order.StaffId,
                StaffName = staffName,
                KitchenStaffId = order.KitchenStaffId,
                KitchenStaffName = kitchenStaffName,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.OrderItemId,
                    MenuItemId = oi.MenuItemId,
                    MenuItemName = oi.MenuItem.Name,
                    Price = oi.MenuItem.Price,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };
        }

        public async Task<Order> UpdateOrderAsync(int orderId, OrderUpdateRequest request)
        {
            var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (existingOrder == null) return null;

            var kitchenStaffExists = await _context.Users.AnyAsync(u => u.UserId == request.KitchenStaffId && u.RoleId == 6);
            if (!kitchenStaffExists)
            {
                throw new Exception("Kitchen staff does not exist.");
            }

            var StaffExists = await _context.Users.AnyAsync(u => u.UserId == request.StaffId && u.RoleId == 4);
            if (!StaffExists)
            {
                throw new Exception("Staff does not exist.");
            }

            var CustomerExists = await _context.Users.AnyAsync(u => u.UserId == request.CustomerId && u.RoleId == 5);
            if (!CustomerExists)
            {
                throw new Exception("Customer does not exist.");
            }

            existingOrder.CustomerId = request.CustomerId;
            existingOrder.StaffId = request.StaffId;
            existingOrder.KitchenStaffId = request.KitchenStaffId;
            existingOrder.Status = request.Status;
            existingOrder.OrderDate = request.OrderDate;

            // Update OrderItems
            existingOrder.OrderItems = new List<OrderItem>();
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

                existingOrder.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();
            return existingOrder;
        }

    }
}
