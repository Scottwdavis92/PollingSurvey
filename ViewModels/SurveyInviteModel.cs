using Microsoft.AspNetCore.Mvc.Rendering;
using PollingApplication.Models;
using System.Collections.ObjectModel;

namespace PollingApplication.ViewModels
{
    public class SurveyInviteModel
    {
        public IEnumerable<SelectListItem>? Employees { get; set; }
        public SurveySessionId? SurveySession { get; set; }

        public int SelectedEmployeeId { get; set; }
    }
}
