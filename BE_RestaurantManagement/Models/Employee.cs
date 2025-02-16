using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_RestaurantManagement.Models
{
    public class Employee
    {
        public ICollection<Shift> Shifts { get; set; }
    }
}
