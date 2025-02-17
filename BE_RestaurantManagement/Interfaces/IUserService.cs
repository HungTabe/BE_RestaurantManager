using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string fullName, string email, string password, string roleId);
        Task<string> AuthenticateAsync(LoginRequest request);
    }
}
