using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2")] // Only role Admin
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            bool result = await _adminService.ResetPasswordAsync(request);
            if (!result)
                return NotFound(new { message = "User not exist!" });

            return Ok(new { message = "Reset password successfully!" });
        }

        [HttpPut("change-role")]
        public async Task<IActionResult> ChangeUserRole(int userId, int newRoleId)
        {
            var result = await _adminService.ChangeUserRoleAsync(userId, newRoleId);
            if (!result)
            {
                return NotFound(new { message = "User not found or role update failed" });
            }

            return Ok(new { message = "User role updated successfully" });
        }
    }
}
