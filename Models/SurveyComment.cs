using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollingApplication.Models
{
    public class SurveyComment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        
        public int SurveySessionId { get; set; }
        public SurveySessionId SurveySession { get; set; }
    }
}
