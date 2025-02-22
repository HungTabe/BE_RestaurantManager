using BE_RestaurantManagement.Data;
using BE_RestaurantManagement.DTOs;
using BE_RestaurantManagement.Interfaces;
using BE_RestaurantManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace BE_RestaurantManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly RestaurantDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public CustomerService(RestaurantDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<Customer> CreateCustomerAsync(CustomerCreateRequest request)
        {
            // If request.Email is null, will be "customer@gmail.com" by default
            string email = request.Email ?? "customer@gmail.com";

            var customer = new Customer
            {
                FullName = request.FullName,
                Email = email,
                Password = _passwordHasher.HashPassword(null, "12345678"),
                RoleId = 5 // 5 = Customer
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Customer>();

            return await _context.Customers
                .Where(c => c.RoleId == 5 &&
                           (c.FullName.Contains(keyword) || c.Email.Contains(keyword)))
                .ToListAsync();
        }
    }
}
