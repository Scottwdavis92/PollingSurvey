using PollingApplication.Models;

namespace PollingApplication.ViewModels
{
    public class SubmitSurvey
    {

        public ICollection<SurveyResponse> surveyResponses { get; set; } = new List<SurveyResponse>();

        public string? surveyComments { get; set; }

        public string SurveyGuid { get; set; } = string.Empty;

        public int employeeId { get; set; }
    }
}
