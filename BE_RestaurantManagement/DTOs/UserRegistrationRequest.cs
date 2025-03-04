using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.DTOs
{
    public class UserRegistrationRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string RoleId { get; set; }

    }
}
