using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_RestaurantManagement.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public int Capacity { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; } // Available, Reserved
    }
}
