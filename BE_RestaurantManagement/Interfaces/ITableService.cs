using BE_RestaurantManagement.DTOs;

namespace BE_RestaurantManagement.Interfaces
{
    public interface ITableService
    {
        Task<TableDTO> CreateTableAsync(CreateTableDTO tableDto);
        Task<TableDTO> UpdateTableAsync(int id, UpdateTableDTO tableDto);
        Task<bool> DeleteTableAsync(int id);
        Task<TableDTO> GetTableByIdAsync(int id);
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> ReserveTableAsync(int id);
        Task<TableDTO> CancelReservationAsync(int id);

    }
}
