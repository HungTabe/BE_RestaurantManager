using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_RestaurantManagement.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly RestaurantDbContext _context;

        public MenuItemService(RestaurantDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả món ăn
        public async Task<List<MenuItem>> GetAllMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        // Lấy món ăn theo ID
        public async Task<MenuItem?> GetMenuItemById(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        // Thêm món ăn mới
        public async Task<MenuItem> AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        // Cập nhật món ăn
        public async Task<MenuItem?> UpdateMenuItem(int id, MenuItem updatedItem)
        {
            var existingItem = await _context.MenuItems.FindAsync(id);
            if (existingItem == null) return null;

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            existingItem.Price = updatedItem.Price;
            existingItem.Category = updatedItem.Category;
            existingItem.IsAvailable = updatedItem.IsAvailable;
            existingItem.ImageUrl = updatedItem.ImageUrl;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        // Xóa món ăn
        public async Task<bool> DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null) return false;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
