using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = BE_RestaurantManagement.DTOs.LoginRequest;
using BE_RestaurantManagement.Services;
using Microsoft.AspNetCore.Authorization;

namespace BE_RestaurantManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
                var user = await _authService.RegisterUserAsync(request.FullName, request.Email, request.Password, request.RoleId);
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
            var authResponse = await _authService.AuthenticateAsync(request);

            if (authResponse == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(authResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Get token from request header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return BadRequest("Token is required");

            var result = _authService.Logout(token);
            if (!result) return BadRequest("Failed to logout");

            return Ok("Logged out successfully");
        }
    }
}
