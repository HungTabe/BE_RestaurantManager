using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(string fullName, string email, string password, string roleId);
        Task<string> AuthenticateAsync(LoginRequest request);
    }
}
