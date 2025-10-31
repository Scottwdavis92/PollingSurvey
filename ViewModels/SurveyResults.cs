using PollingApplication.Models;
using System.Collections.ObjectModel;

namespace PollingApplication.ViewModels
{
    public class SurveyResults
    {
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public ObservableCollection<SurveyComment> SurveyComments { get;} = new ObservableCollection<SurveyComment>();
        public ObservableCollection<SurveyResponse> SurveyResponses { get;} = new ObservableCollection<SurveyResponse>();

    }
}
