using BE_RestaurantManagement.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.RegisterUserAsync(request.FullName, request.Email, request.Password, request.RoleId);
                return Ok(new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Password,
                    user.RoleId
                    
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.AuthenticateAsync(request);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { token });
        }
    }
}
