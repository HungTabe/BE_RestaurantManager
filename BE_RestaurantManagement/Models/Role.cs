using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_RestaurantManagement.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
