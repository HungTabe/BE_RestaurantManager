using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_RestaurantManagement.Controllers
{
    [Authorize]
    [Route("api/tables")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }


        [Authorize(Roles = "1,4")]
        [HttpGet("get-all-table")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(new { message = "Successfully retrieved all tables!", tables });
        }

        [Authorize(Roles = "1")]
        [HttpPost("create-table")]
        public async Task<ActionResult<TableDTO>> CreateTable([FromBody] CreateTableDTO tableDto)
        {
            try
            {
                var createdTable = await _tableService.CreateTableAsync(tableDto);
                return CreatedAtAction(nameof(GetTableById), new { id = createdTable.TableId }, new
                {
                    message = "Table created successfully!",
                    table = createdTable
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("update-table/{id}")]
        public async Task<ActionResult<TableDTO>> UpdateTable(int id, [FromBody] UpdateTableDTO tableDto)
        {
            try
            {
                var updatedTable = await _tableService.UpdateTableAsync(id, tableDto);
                return Ok(new { message = $"Successfully updated table ID {id}!", table = updatedTable });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("delete-table/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            try
            {
                await _tableService.DeleteTableAsync(id);
                return Ok(new { message = $"Successfully deleted table ID {id}!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "1,4")]
        [HttpGet("detail-table/{id}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int id)
        {
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
                return Ok(new { message = "Successfully retrieved table information!", table });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "1,4")]
        [HttpPut("reserve-table/{id}")]
        public async Task<ActionResult<TableDTO>> ReserveTable(int id)
        {
            try
            {
                var reservedTable = await _tableService.ReserveTableAsync(id);
                return Ok(new { message = $"Successfully reserved table with ID {id}!", table = reservedTable });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "1,4")]
        [HttpPut("cancel-reservation-table/{id}")]
        public async Task<ActionResult<TableDTO>> CancelReservation(int id)
        {
            try
            {
                var cancelledTable = await _tableService.CancelReservationAsync(id);
                return Ok(new { message = $"Successfully canceled table reservation with ID {id}!", table = cancelledTable });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
