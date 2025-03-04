using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Tên chương trình khuyến mãi

        [StringLength(500)]
        public string Description { get; set; } // Mô tả chương trình

        [Required]
        public decimal DiscountPercentage { get; set; } // Mức giảm giá (ví dụ: 10% -> 0.1)

        [Required]
        public DateTime StartDate { get; set; } // Ngày bắt đầu

        [Required]
        public DateTime EndDate { get; set; } // Ngày kết thúc

        public bool IsActive { get; set; } = true; // Trạng thái có hiệu lực không

        // Quan hệ 1-N: Một chương trình khuyến mãi có thể áp dụng cho nhiều đơn hàng
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
