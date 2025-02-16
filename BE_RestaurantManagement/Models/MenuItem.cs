using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_RestaurantManagement.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Category { get; set; } // VD: Đồ uống, Món chính

        public bool IsAvailable { get; set; } = true; // Món có sẵn không?

        [Column(TypeName = "nvarchar(255)")]
        public string ImageUrl { get; set; } // Hình ảnh món ăn

        // Một món ăn có thể xuất hiện trong nhiều đơn hàng
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
