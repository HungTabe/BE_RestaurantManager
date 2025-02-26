using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_RestaurantManagement.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; } // Pending, Cooking, Completed

        // StaffId to know which staff progress this order
        public int? StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; } // Quan hệ với Staff

        public int? KitchenStaffId { get; set; } // Kitchen staff processing orders
        [ForeignKey("KitchenStaffId")]
        public KitchenStaff KitchenStaff { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Payment Payment { get; set; }
    }
}
