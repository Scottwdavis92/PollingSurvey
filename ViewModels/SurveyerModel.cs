using Microsoft.AspNetCore.Mvc.Rendering;
using PollingApplication.Models;

namespace PollingApplication.ViewModels
{
    public class SurveyerModel
    {
        public IEnumerable<Question> genQuestions { get; set; } = new List<Question>();
        public IEnumerable<Question> deptQuestions { get; set; } = new List<Question>();

        public IEnumerable<SelectListItem> ScaleValues { get; set; } = new List<SelectListItem>();

        public string SurveyGuid { get; set; } = string.Empty;

        public int EmployeeId { get; set; }

    }
}
