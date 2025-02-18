using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("search")]
        public IActionResult SearchUsers([FromQuery] string keyword)
        {
          
            var users = _userService.SearchUsers(keyword);
            return Ok(users);
        }

    }
}
