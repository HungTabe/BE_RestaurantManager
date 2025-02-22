using BE_RestaurantManagement.DTOs;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IAdminService
    {
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<bool> ChangeUserRoleAsync(int userId, int newRoleId);

    }
}
