using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.DTO;
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

        public async Task<List<SurveyResponse>> GetAllSurveyResponseAsync()
        {
              var surveyResponses = await _context.SurveyResponses
                .Include(sr => sr.Question)
                .Include(sr => sr.Employee).ThenInclude(e => e.Department)
                .Include(sr => sr.ResponseScale)
                .Include(sr => sr.SurveySession)
                .ToListAsync();
            _logger.LogInformation("Fetched {Count} survey responses.", surveyResponses.Count);
            return surveyResponses; 
        }

        public async Task<List<SurveyComment>> GetAllSurveyCommentsAsync()
        {
              var surveyComments = await _context.SurveyComments
                .Include(sc => sc.Employee)
                .Include(sc => sc.SurveySession)
                .ToListAsync();
            _logger.LogInformation("Fetched {Count} survey comments.", surveyComments.Count);
            return surveyComments;
        }

        public async Task<List<GeneralQuestionAverage>> GetAverageQuestionScore()
        {
            var allGenQenScore = await _context.SurveyResponses.GroupBy(q => new { 
                q.Question.Questions,
                q.Question.Department.DepartmentName
            }).Select(q =>  new GeneralQuestionAverage
            {
            QuestionText = q.Key.Questions,
            DepartmentName = q.Key.DepartmentName,
                AverageScore = (int)q.Average(s => s.ScaleValue),
            
            }).ToListAsync();

            return allGenQenScore;

        }

        public async Task<List<EmployeeQuestionAverage>> EmployeeQuestionAverage()
        {
            var employeeQenScore = await _context.SurveyResponses.GroupBy(q => new {
                q.Question.Questions,
                q.Employee.FirstName,
                q.Employee.LastName,
                q.Employee.EmployeeId,
                q.Employee.Department.DepartmentName,
                q.Question.ScopeType
            }).Select(q => new EmployeeQuestionAverage
            {
                QuestionText = q.Key.Questions,
                EmployeeName = $"{q.Key.FirstName} {q.Key.LastName}",
                EmployeeId = q.Key.EmployeeId,
                DepartmentName = q.Key.DepartmentName,
                DepartmentType = q.Key.ScopeType,
                AverageScore = (int)q.Average(s => s.ScaleValue),
            }).ToListAsync();

            return employeeQenScore;
        }

        public async Task<List<EmployeeDepartmentSurveyResults>> EmployeeDepartmentQuestionAverage()
        {
            var employeeQenScore = await _context.SurveySessions.Select(q => new EmployeeDepartmentSurveyResults
            {

                EmployeeName = $"{q.Employee.FirstName} {q.Employee.LastName}",
                EmployeeId = q.Employee.EmployeeId,
                DepartmentName = q.Employee.Department.DepartmentName,

                StartTime = q.StartTime,
                EndTime = q.EndTime,
                IsCompleted = q.Completed ? "Yes" : "No",

                SessionId = q.Id

            }).ToListAsync();

            return employeeQenScore;
        }

        public async Task<List<SurveySessionId>> GetAllSurveySessionIdsAsync()
        {
            var surveySessionIds = await _context.SurveySessions
                .Include(ss => ss.Employee)
                .ToListAsync();
            _logger.LogInformation("Fetched {Count} survey session IDs.", surveySessionIds.Count);
            return surveySessionIds;
        }

        public async Task<bool> DeleteSurveySessionsAsync(int surveySessionId)
        {
            var surveySession = await _context.SurveySessions
                .FirstOrDefaultAsync(ss => ss.Id == surveySessionId);

            var comments = await _context.SurveyComments
                .Where(sc => sc.SurveySessionId == surveySessionId)
                .FirstOrDefaultAsync();
            if (surveySession != null)
            {
                if (comments != null)
                {
                    _context.SurveyComments.Remove(comments);
                    
                    _logger.LogInformation("Deleted comments for survey session ID: {SurveySessionId}", surveySessionId);
                }

                _context.SurveySessions.Remove(surveySession);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Deleted survey session with ID: {SurveySessionId}", surveySessionId);
                return true;

            }
            else
            {
                _logger.LogWarning("Survey session with ID: {SurveySessionId} not found for deletion.", surveySessionId);
                return false;

            }
            
        }
    }
}
