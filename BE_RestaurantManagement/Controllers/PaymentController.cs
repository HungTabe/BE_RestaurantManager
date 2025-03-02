using BE_RestaurantManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2,3,4")]
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // API xử lý thanh toán
        [HttpPost("payment-request")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            try
            {
                var payment = await _paymentService.ProcessPaymentAsync(request.OrderId, request.PaymentMethod);
                return Ok(new { message = "Payment successful", payment });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    // DTO nhận request từ frontend
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } // Cash, Card, VNPAY
    }
}
