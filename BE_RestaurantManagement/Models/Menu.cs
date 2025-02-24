using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BE_RestaurantManagement.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
