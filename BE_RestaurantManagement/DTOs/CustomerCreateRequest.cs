using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.DTOs
{
    public class CustomerCreateRequest
    {
        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
