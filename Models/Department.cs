using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PollingApplication.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [MaxLength(100)]
        public string DepartmentDescription { get; set; }

        
        public ICollection<Employee> Employees { get; set;} = new List<Employee>();

    }
}
