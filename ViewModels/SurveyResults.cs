using PollingApplication.DTO;
using PollingApplication.Models;
using System.Collections.ObjectModel;

namespace PollingApplication.ViewModels
{
    public class SurveyResults
    {
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public IEnumerable<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
        public IEnumerable<SurveyComment> SurveyComments { get;set; } = new List<SurveyComment>();

        public IEnumerable<GeneralQuestionAverage> GeneralQuestionAverages { get; set; } = new List<GeneralQuestionAverage>();

        public IEnumerable<EmployeeQuestionAverage> EmployeeQuestionAverages { get; set; } = new List<EmployeeQuestionAverage>();

    }
}
