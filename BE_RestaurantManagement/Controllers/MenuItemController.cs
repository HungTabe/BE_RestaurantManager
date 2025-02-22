using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize(Roles = "2")] // Chỉ Admin có quyền truy cập API này
    [Route("api/menuitems")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // Lấy tất cả món ăn
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            return Ok(await _menuItemService.GetAllMenuItems());
        }

        // Lấy món ăn theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await _menuItemService.GetMenuItemById(id);
            if (item == null) return NotFound("Menu item not found.");
            return Ok(item);
        }

        // Thêm món ăn mới
        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] MenuItem menuItem)
        {
            var newItem = await _menuItemService.AddMenuItem(menuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = newItem.MenuItemId }, newItem);
        }

        // Cập nhật món ăn
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItem updatedItem)
        {
            var item = await _menuItemService.UpdateMenuItem(id, updatedItem);
            if (item == null) return NotFound("Menu item not found.");
            return Ok(item);
        }

        // Xóa món ăn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var success = await _menuItemService.DeleteMenuItem(id);
            if (!success) return NotFound("Menu item not found.");
            return NoContent();
        }
    }
}
