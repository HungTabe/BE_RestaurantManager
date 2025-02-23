using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using BE_RestaurantManagement.Interfaces;

namespace BE_RestaurantManagement.Services
{
    public class StaffService : IStaffService
    {
        private readonly RestaurantDbContext _context;

        public StaffService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllStaffAsync()
        {
            return await _context.Users
                .Select(s => new StaffDTO
                {
                    UserId = s.UserId,
                    FullName = s.FullName,
                    Email = s.Email,
                    RoleId = s.RoleId
                })
                .ToListAsync();
        }

        public async Task<StaffDTO> GetStaffByIdAsync(int id)
        {
            var staff = await _context.Users.FindAsync(id);
            if (staff == null) return null;

            return new StaffDTO
            {
                UserId = staff.UserId,
                FullName = staff.FullName,
                Email = staff.Email,
                RoleId = staff.RoleId
            };
        }

        public async Task<StaffDTO> CreateStaffAsync(CreateStaffDTO staffDto)
        {
            // Kiểm tra xem email hoặc tên đã tồn tại chưa
            var existingStaff = await _context.Users
                .FirstOrDefaultAsync(s => s.Email == staffDto.Email || s.FullName == staffDto.FullName);

            if (existingStaff != null)
            {
                return null; // Trả về null để báo lỗi nhân viên đã tồn tại
            }

            var staff = new Staff
            {
                FullName = staffDto.FullName,
                Email = staffDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(staffDto.Password),
                RoleId = 4 // Giả định RoleId = 4 là nhân viên
            };

            _context.Users.Add(staff);
            await _context.SaveChangesAsync();

            return new StaffDTO
            {
                UserId = staff.UserId,
                FullName = staff.FullName,
                Email = staff.Email,
                RoleId = staff.RoleId
            };
        }

        public async Task<StaffDTO> UpdateStaffAsync(int id, CreateStaffDTO staffDto)
        {
            var staff = await _context.Users.FindAsync(id);
            if (staff == null) return null;

            // Kiểm tra nếu không có sự thay đổi nào
            if (staff.FullName == staffDto.FullName && staff.Email == staffDto.Email && BCrypt.Net.BCrypt.Verify(staffDto.Password, staff.Password))
            {
                return null; // Trả về null nếu không có thay đổi
            }

            // Cập nhật thông tin nhân viên
            staff.FullName = staffDto.FullName;
            staff.Email = staffDto.Email;
            staff.Password = BCrypt.Net.BCrypt.HashPassword(staffDto.Password);

            _context.Users.Update(staff);
            await _context.SaveChangesAsync();

            return new StaffDTO
            {
                UserId = staff.UserId,
                FullName = staff.FullName,
                Email = staff.Email,
                RoleId = staff.RoleId
            };
        }


        public async Task<bool> DeleteStaffAsync(int id)
        {
            var staff = await _context.Users.FindAsync(id);
            if (staff == null) return false;

            _context.Users.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
