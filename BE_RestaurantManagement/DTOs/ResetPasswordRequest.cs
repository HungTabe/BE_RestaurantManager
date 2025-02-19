namespace BE_RestaurantManagement.DTOs
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }  // ID of User need reset pass
        public string NewPassword { get; set; }
    }
}
