using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class DeparmentFetch
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;
        public DeparmentFetch(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            try
            {
                return await _context.Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching departments.");
                return new List<Department>();
            }
        }

        public async Task<Department?> GetDepartmentByIdAsync(int departmentId)
        {
            try
            {
                return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the department with ID {DepartmentId}.", departmentId);
                return null;
            }
        }
    }
}
