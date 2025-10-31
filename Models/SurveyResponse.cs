using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollingApplication.Models
{
    public class SurveyResponse
    {
        [Key]
        public int Id { get; set; }
        
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("ResponseScale")]
        public int ScaleValue { get; set; }
        public ResponseScale ResponseScale { get; set; }

        
        public int SurveySessionId { get; set; }
        public SurveySessionId SurveySession { get; set; } 

    }
}
