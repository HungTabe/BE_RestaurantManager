using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }

        [Required]
        public int StaffId { get; set; } // Thêm StaffId để tạo quan hệ

        [ForeignKey("StaffId")]
        public Staff Staff { get; set; } // Quan hệ với Staff

        public int HoursWorked { get; set; }
    }
}
