using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace BE_RestaurantManagement.Interfaces
{
    public interface IUserService
    {
        // Only for user have roleId = 2 (Admin);
        IEnumerable<User> SearchUsers(string keyword);
    }
}
