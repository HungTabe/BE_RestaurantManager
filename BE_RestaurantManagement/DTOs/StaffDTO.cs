namespace BE_RestaurantManagement.DTOs
{
    public class StaffDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }

    public class CreateStaffDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Password sẽ được hash trước khi lưu
    }
}
