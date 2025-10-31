using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollingApplication.Models
{
    public class SurveySessionId
    {
        [Key]
        public int Id { get; set; }
        public Guid SurveyGuid { get; set; }
       
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool Completed { get; set; }

        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
    }
}
