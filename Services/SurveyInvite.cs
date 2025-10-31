using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class SurveyInvite
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;
        public SurveyInvite(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<SurveySessionId> CreateSurveyInvite(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found. Cannot create survey invite.", employeeId);
                throw new ArgumentException($"Employee with ID {employeeId} not found.");
            }
            var surveyInvite = new SurveySessionId
            {
                EmployeeId = employeeId,
                SurveyGuid = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                Completed = false
            };
            _context.SurveySessions.Add(surveyInvite);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Created survey invite for EmployeeId: {EmployeeId} with SurveyGuid: {SurveyGuid}", employeeId, surveyInvite.SurveyGuid);
            return surveyInvite;
        }

        public async Task<SurveySessionId?> GetSurveyInviteByGuidAsync(Guid surveyGuid)
        {
            return await _context.SurveySessions
                .Include(ss => ss.Employee)
                .FirstOrDefaultAsync(ss => ss.SurveyGuid == surveyGuid);
        }
    }
}
