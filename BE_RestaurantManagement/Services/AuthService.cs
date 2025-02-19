using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_RestaurantManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly RestaurantDbContext _context;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache; // Cache memo to save blocked token list



        public AuthService(RestaurantDbContext context, IConfiguration config, IMemoryCache cache)
        {
            _context = context;
            _config = config;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        }

        public async Task<User> RegisterUserAsync(string fullName, string email, string password, string roleId)
        {
            // Kiểm tra xem email đã tồn tại hay chưa
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new Exception("Email is already registered.");
            }

            // Hash password (sử dụng BCrypt, cài package BCrypt.Net-Next)
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = passwordHash,
                RoleId = 2, // Mặc định khi register vào thì RoleId=2 là user thông thường
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null; // Sai email hoặc mật khẩu
            }

            // Kiểm tra mật khẩu đã mã hóa so với mật khẩu người dùng nhập vào
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null; // Mật khẩu không chính xác
            }


            return GenerateJwtToken(user); // Tạo token sau khi xác thực thành công
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())


            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool Logout(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            // Save token into cache blacklist
            var expiration = DateTime.UtcNow.AddMinutes(30); // Token outdate after 30 mins
            _cache.Set(token, "blacklisted", new MemoryCacheEntryOptions { AbsoluteExpiration = expiration });

            return true;
        }



    }
}
