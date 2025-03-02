using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderCreateRequest request);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderGetAllRequest>> GetAllOrdersAsync();
        Task<Order> UpdateOrderAsync(int orderId, OrderUpdateRequest order);
        Task<bool> DeleteOrderAsync(int orderId);

    }
}
