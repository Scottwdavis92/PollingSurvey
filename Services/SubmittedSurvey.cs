using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class SubmittedSurvey
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;

        public SubmittedSurvey(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task SaveSubmittedResponseSurveyAsync(SurveyResponse surveyResponse)
        {
            if (surveyResponse == null)
            {
                _logger.LogWarning("Attempted to save a null survey response.");
                throw new ArgumentNullException(nameof(surveyResponse), "Survey response cannot be null.");
            }

            _context.SurveyResponses.Add(surveyResponse);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saved submitted survey for SurveySessionId: {SurveySessionId}", surveyResponse.SurveySessionId);
        }

        public async Task SaveSubmittedCommentAsync(string comment, int surveySessionId, int employeeId)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                _logger.LogWarning("Attempted to save an empty or whitespace comment for SurveySessionId: {SurveySessionId}", surveySessionId);
                throw new ArgumentException("Comment cannot be empty or whitespace.", nameof(comment));
            }

            var surveyComment = new SurveyComment
            {
                SurveySessionId = surveySessionId,
                Comment = comment,
                EmployeeId = employeeId
            };

            _context.SurveyComments.Add(surveyComment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Saved submitted comment for SurveySessionId: {SurveySessionId}", surveySessionId);

        }
    }
}
