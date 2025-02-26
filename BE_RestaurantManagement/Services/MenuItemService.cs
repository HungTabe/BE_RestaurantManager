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

        // Get all Menu
        public async Task<List<MenuItem>> GetAllMenuItems()
        {
            return await _context.MenuItems
                .Select(m => new MenuItem
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    Category = m.Category,
                    IsAvailable = m.IsAvailable,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        // Get menu by ID
        public async Task<MenuItem?> GetMenuItemById(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        // Add new Menu
        public async Task<MenuItem> AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        // Update Menu
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

        // Delete Menu
        public async Task<bool> DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null) return false;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<MenuItem>> SearchMenuItems(string query)
        {
            return await _context.MenuItems
                .Where(m => m.Name.Contains(query) || m.Category.Contains(query))
                .ToListAsync();
        }
    }
}
