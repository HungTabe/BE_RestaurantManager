using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderCreateRequest request);

    }
}
