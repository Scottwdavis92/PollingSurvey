using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollingApplication.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        
        public int Departmentid { get; set; }
        public Department Department { get; set; }

        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
        public ICollection<SurveyComment> SurveyComments { get; set; } = new List<SurveyComment>();
    }
}
