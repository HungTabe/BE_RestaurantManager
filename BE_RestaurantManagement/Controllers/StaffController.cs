using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_RestaurantManagement.Controllers
{

    [Authorize(Roles = "2,3")]
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaff()
        {
            return Ok(await _staffService.GetAllStaffAsync());
        }

        [HttpGet("get-staff-by-id/{id}")]
        public async Task<ActionResult<StaffDTO>> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound(new { message = "Nhân viên không tồn tại!" });

            return Ok(staff);
        }

        [HttpPost("create-staff")]
        public async Task<ActionResult<StaffDTO>> CreateStaff([FromBody] CreateStaffDTO staffDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdStaff = await _staffService.CreateStaffAsync(staffDto);
            if (createdStaff == null) return BadRequest(new { message = "Email hoặc tên nhân viên đã tồn tại!" });

            return CreatedAtAction(nameof(GetStaffById), new { id = createdStaff.UserId }, createdStaff);
        }

        [HttpPut("update-staff/{id}")]
        public async Task<ActionResult<StaffDTO>> UpdateStaff(int id, [FromBody] CreateStaffDTO staffDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedStaff = await _staffService.UpdateStaffAsync(id, staffDto);
            if (updatedStaff == null) return BadRequest(new { message = "Không có thay đổi nào được thực hiện hoặc nhân viên không tồn tại!" });

            return Ok(new { message = "Cập nhật nhân viên thành công!", staff = updatedStaff });
        }


        [HttpDelete("delete-staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _staffService.DeleteStaffAsync(id);
            if (!result) return NotFound(new { message = "Nhân viên không tồn tại!" });

            return Ok(new { message = "Xóa nhân viên thành công!" });
        }

    }
}
