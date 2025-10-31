using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class FetchSurveyResults
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;
        public FetchSurveyResults(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }
    }
}
