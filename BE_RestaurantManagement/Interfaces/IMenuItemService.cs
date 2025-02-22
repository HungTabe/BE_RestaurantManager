using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IMenuItemService
    {
        Task<List<MenuItem>> GetAllMenuItems();
        Task<MenuItem?> GetMenuItemById(int id);
        Task<MenuItem> AddMenuItem(MenuItem menuItem);
        Task<MenuItem?> UpdateMenuItem(int id, MenuItem updatedItem);
        Task<bool> DeleteMenuItem(int id);
    }
}
