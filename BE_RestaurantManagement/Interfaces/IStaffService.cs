using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDTO>> GetAllStaffAsync();
        Task<StaffDTO> GetStaffByIdAsync(int id);
        Task<StaffDTO> CreateStaffAsync(CreateStaffDTO staffDto);
        Task<StaffDTO> UpdateStaffAsync(int id, CreateStaffDTO staffDto);
        Task<bool> DeleteStaffAsync(int id);
    }
}
