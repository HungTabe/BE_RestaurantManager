using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2,3,4")] // Only role Admin
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(request);
                return Ok(new { message = "Customer created successfully", customer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search-customer")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string keyword)
        {
            try
            {
                var customers = await _customerService.SearchCustomersAsync(keyword);
                return Ok(new { message = "Search results", customers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
