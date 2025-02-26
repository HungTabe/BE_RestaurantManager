using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2,3,4")] // Admin/Manager/Staff
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(request);
                return Ok(new { message = "Order created successfully", order });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-order-by-id/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Order not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
            }
        }
    }
}
