using BE_RestaurantManagement.DTOs;
using RestaurantAPI.Models;

namespace BE_RestaurantManagement.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(CustomerCreateRequest request);

        Task<IEnumerable<Customer>> SearchCustomersAsync(string keyword);


    }
}
