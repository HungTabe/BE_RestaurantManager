using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(int orderId, string paymentMethod);

    }
}
