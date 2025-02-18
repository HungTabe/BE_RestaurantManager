using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BE_RestaurantManagement.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext _context;
        private readonly IConfiguration _config;


        public UserService(RestaurantDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IEnumerable<User> SearchUsers(string keyword)
        {
            return _context.Users
                .Where(u => u.FullName.Contains(keyword) || u.Email.Contains(keyword))
                .ToList();
        }

    }
}
