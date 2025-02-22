namespace BE_RestaurantManagement.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
