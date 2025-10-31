using System.ComponentModel.DataAnnotations;

namespace PollingApplication.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Questions { get; set; }
        public string ScopeType { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
