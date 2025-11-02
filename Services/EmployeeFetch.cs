using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.DTO;
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

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the employee with ID {EmployeeId}.", employeeId);
                return null;
            }
        }

        public async Task<bool> CreateEmployee(AddEmployee newEmployee)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = newEmployee.EmployeeFirstName,
                    LastName = newEmployee.EmployeeLastName,
                    Email = newEmployee.EmployeeEmail,
                    Departmentid = newEmployee.DepartmentId
                    
                };
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new employee.");
                return false;
            }
        }

    }
}
