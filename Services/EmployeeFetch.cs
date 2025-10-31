using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class EmployeeFetch
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly PollingContext _context;

        public EmployeeFetch(ILogger<SurveyInvite> logger, PollingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employees.");
                return new List<Employee>();
            }
        }

    }
}
