using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2")] // Just Admin can access this API
    [Route("api/menuitem")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // Get all menu items (Accessible by both Users & Admins)
        [HttpGet("get-all-menuitems")]
        [AllowAnonymous] // Allow users to view the menu
        public async Task<IActionResult> GetAllMenuItems()
        {
            return Ok(await _menuItemService.GetAllMenuItems());
        }

        // Get menu item by ID (Accessible by both Users & Admins)
        [HttpGet("get-menuitem-by-id/{id}")]
        [AllowAnonymous] // Allow users to view menu item details
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await _menuItemService.GetMenuItemById(id);
            if (item == null) return NotFound("Menu item not found.");
            return Ok(item);
        }

        // Search for menu items by name, category, or price (Accessible by both Users & Admins)
        [HttpGet("search-menuitem")]
        [AllowAnonymous] // Allow users to search for menu items
        public async Task<IActionResult> SearchMenuItems([FromQuery] string query)
        {
            var result = await _menuItemService.SearchMenuItems(query);
            return Ok(result);
        }

        // Add a new menu item (Only Admin)
        [HttpPost("add-new-menuitem")]
        public async Task<IActionResult> AddMenuItem([FromBody] MenuItem menuItem)
        {
            var newItem = await _menuItemService.AddMenuItem(menuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = newItem.MenuItemId }, newItem);
        }

        // Update menu item details (Only Admin)
        [HttpPut("update-menuitem-by-id/{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItem updatedItem)
        {
            var item = await _menuItemService.UpdateMenuItem(id, updatedItem);
            if (item == null) return NotFound("Menu item not found.");
            return Ok(item);
        }

        // Delete a menu item (Only Admin)
        [HttpDelete("delete-menuitem-by-id/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var success = await _menuItemService.DeleteMenuItem(id);
            if (!success) return NotFound("Menu item not found.");
            return NoContent();
        }
    }
}
