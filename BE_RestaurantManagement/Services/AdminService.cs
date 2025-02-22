using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using System;

namespace BE_RestaurantManagement.Services
{
    public class AdminService : IAdminService
    {
        private readonly RestaurantDbContext _context;

        public AdminService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return false; // Can't find user

            // BCryptv newPass
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeUserRoleAsync(int userId, int newRoleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.RoleId = newRoleId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
