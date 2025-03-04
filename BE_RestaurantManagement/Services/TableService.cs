using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BE_RestaurantManagement.Services
{
    public class TableService : ITableService
    {
        private readonly RestaurantDbContext _context;

        public TableService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            return await _context.Tables
                .Select(t => new TableDTO
                {
                    TableId = t.TableId,
                    Capacity = t.Capacity,
                    Status = t.Status
                })
                .ToListAsync();
        }

        public async Task<TableDTO> CreateTableAsync(CreateTableDTO tableDto)
        {
            var table = new Table { Capacity = tableDto.Capacity, Status = "Available" };
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return new TableDTO { TableId = table.TableId, Capacity = table.Capacity, Status = table.Status };
        }

        public async Task<TableDTO> UpdateTableAsync(int id, UpdateTableDTO tableDto)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) throw new KeyNotFoundException($"Không tìm thấy bàn ăn với ID {id}.");

            table.Capacity = tableDto.Capacity;
            table.Status = tableDto.Status;
            await _context.SaveChangesAsync();

            return new TableDTO { TableId = table.TableId, Capacity = table.Capacity, Status = table.Status };
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) throw new KeyNotFoundException($"Không tìm thấy bàn ăn với ID {id}.");

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TableDTO> GetTableByIdAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) throw new KeyNotFoundException($"Không tìm thấy bàn ăn với ID {id}.");
            return new TableDTO { TableId = table.TableId, Capacity = table.Capacity, Status = table.Status };
        }

        public async Task<TableDTO> ReserveTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) throw new KeyNotFoundException($"Không tìm thấy bàn ăn với ID {id}.");
            if (table.Status == "Reserved") throw new InvalidOperationException($"Bàn {id} đã được đặt trước!");

            table.Status = "Reserved";
            await _context.SaveChangesAsync();
            return new TableDTO { TableId = table.TableId, Capacity = table.Capacity, Status = table.Status };
        }

        public async Task<TableDTO> CancelReservationAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) throw new KeyNotFoundException($"Không tìm thấy bàn ăn với ID {id}.");
            if (table.Status != "Reserved") throw new InvalidOperationException($"Bàn {id} chưa được đặt trước, không thể hủy!");

            table.Status = "Available";
            await _context.SaveChangesAsync();
            return new TableDTO { TableId = table.TableId, Capacity = table.Capacity, Status = table.Status };
        }
    }
}
