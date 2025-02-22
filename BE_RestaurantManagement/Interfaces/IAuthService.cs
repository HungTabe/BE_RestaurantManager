using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(string fullName, string email, string password, string roleId);
        Task<AuthResponse> AuthenticateAsync(LoginRequest request);
        bool Logout(string token);

    }
}
