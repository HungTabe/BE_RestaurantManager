using Azure.Core;
using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_RestaurantManagement.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly RestaurantDbContext _context;

        public PaymentService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcessPaymentAsync(int orderId, string paymentMethod)
        {
            // 1. Validate Order
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                throw new Exception("Order not found");

            if (order.Status == "Paid")
                throw new Exception("Order has already been paid");

            decimal totalAmount = order.OrderItems.Sum(item => item.TotalPrice);

            if (totalAmount <= 0)
                throw new Exception("Invalid total amount");

            // 2. Create Payment Record
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount = totalAmount,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending",
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // 3. Handle Payment Methods
            if (paymentMethod == "Cash")
            {
                payment.PaymentStatus = "Complete";
                payment.PaymentStatus = "Paid";
            }
            else if (paymentMethod == "ZaloPay")
            {
                // Call ZaloPay API for processing (Placeholder)
                //bool paymentSuccess = await _paymentService.ProcessZaloPay(order.OrderId, totalAmount);
                bool paymentSuccess = true;

                if (paymentSuccess)
                {
                    payment.PaymentStatus = "Complete";
                }
                else
                {
                    payment.PaymentStatus = "Fail";
                }
            }
            else
            {
                throw new Exception("Unsupported payment method");
            }

            await _context.SaveChangesAsync();

            // 4. Return Payment Response
            return new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                OrderId = order.OrderId,
                Amount = totalAmount,
                PaymentStatus = payment.PaymentStatus
            };
        }
    }

    internal class PaymentResponse : Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
