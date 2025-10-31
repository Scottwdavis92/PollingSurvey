using System.ComponentModel.DataAnnotations;

namespace PollingApplication.Models
{
    public class ResponseScale
    {
        [Key]
        public int ScaleValue { get; set; }

        [Required]
        public string ScaleDescription { get; set; }

        public ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
