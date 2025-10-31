using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class QuestionFetch
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;

        public QuestionFetch(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            try
            {
                return await _context.Questions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return new List<Question>();
            }
        }

        public async Task<List<Question>> GetGeneralQuestionsAsync()
        {
            try
            {
                return await _context.Questions.Where(a => a.DepartmentId == 0).Include(d => d.Department).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return new List<Question>();
            }
        }

        public async Task<List<Question>> GetDepartmentQuestionsAsync(int departmentId)
        {
            try
            {
                return await _context.Questions.Where(a => a.DepartmentId == departmentId).Include(d => d.Department).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return new List<Question>();
            }
        }

        public async Task<List<Question>> GetQuestionsAsync(int departmentId)
        {
            try
            {
                var genQuestions = await _context.Questions.Where(a => a.DepartmentId == 0).ToListAsync();
                var depQuestions = await _context.Questions.Where(a => a.DepartmentId == departmentId).ToListAsync();

                var allQuestions = genQuestions.Concat(depQuestions).ToList();

                return allQuestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return new List<Question>();
            }
        }

        public async Task<List<ResponseScale>> GetReponseScale()
        {
            try
            {
                var scale = await _context.ResponseScales.ToListAsync();
                return scale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return new List<ResponseScale>();
            }
        }
    }
}
