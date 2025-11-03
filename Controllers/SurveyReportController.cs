using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Tls;
using PollingApplication.DTO;
using PollingApplication.Models;
using PollingApplication.Services;
using PollingApplication.ViewModels;
using System.Diagnostics;

namespace PollingApplication.Controllers
{
    [Authorize]
    public class SurveyReportController : Controller
    {
        private readonly ILogger<SurveyReportController> _logger;
        private readonly SurveyInvite _surveyInvite;
        private readonly EmployeeFetch _employeeFetch;
        private readonly Mailer _mailer;
        private readonly FetchSurveyResults _fetchSurveyResults;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DeparmentFetch _deparmentFetch;
        private readonly CreateAdmin _createAdmin;

        public SurveyReportController(ILogger<SurveyReportController> logger, SurveyInvite surveyInvite, EmployeeFetch employeeFetch, Mailer mailer, FetchSurveyResults fetchSurveyResults, UserManager<ApplicationUser> userManager, DeparmentFetch deparmentFetch, CreateAdmin createAdmin)
        {
            _logger = logger;
            _mailer = mailer;
            _surveyInvite = surveyInvite;
            _employeeFetch = employeeFetch;
            _fetchSurveyResults = fetchSurveyResults;
            _userManager = userManager;
            _deparmentFetch = deparmentFetch;
            _createAdmin = createAdmin;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeeResponces = await _fetchSurveyResults.GetAllSurveyResponseAsync();
            var employeeComments = await _fetchSurveyResults.GetAllSurveyCommentsAsync();

            var averageGenQenScore = await _fetchSurveyResults.GetAverageQuestionScore();

            var averageEmpQenScore = await _fetchSurveyResults.EmployeeQuestionAverage();

            var viewModel = new SurveyResults
            {

                SurveyComments = employeeComments,
                GeneralQuestionAverages = averageGenQenScore,
                EmployeeQuestionAverages = averageEmpQenScore


            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SurveyAdminTools()
        {
            var employees = await _employeeFetch.GetAllEmployeesAsync();

            if (employees == null)
            {
                _logger.LogError("No employees found.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var employeeSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.FirstName} {e.LastName} ({e.Email})"
            }).ToList();

            var departments = await _deparmentFetch.GetAllDepartmentsAsync();

            if (departments == null)
            {
                _logger.LogError("No departments found.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var departmentSelectList = departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();

            var viewModel = new SurveyInviteModel
            {
                Employees = employeeSelectList,
                Departments = departmentSelectList
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendSurveyInvite(SurveyInviteModel SelectedEmployee)
        {
            try
            {
                var invitation = await _surveyInvite.CreateSurveyInvite(SelectedEmployee.SelectedEmployeeId);

                var surveyUrl = Url.Action("Survey", "Home", new { token = invitation.SurveyGuid }, Request.Scheme);

                var emailstatus = await _mailer.SendEmailAsync(invitation, surveyUrl);



                ViewBag.Message = $"Survey invite created successfully for employee {SelectedEmployee.SelectedEmployeeId}.";
            }
            catch (ArgumentException ex)
            {
                ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index", "SurveyReport");
        }

        //not recommended only to use for test and to demo
        [HttpPost]
        public async Task<IActionResult> GiveAdminAccess(SurveyInviteModel SelectedEmployee)
        {
            try
            {
                var employee = await _employeeFetch.GetEmployeeByIdAsync(SelectedEmployee.SelectedEmployeeId);

                if (employee == null)
                {
                    ViewBag.Error = "Employee not found.";
                    return RedirectToAction("Index", "SurveyReport");
                }

                bool success = await _createAdmin.AddAdminRoleAndUserAsync(employee);

                if (success)
                {
                    ViewBag.Message = $"Admin access granted successfully to employee ID {SelectedEmployee.SelectedEmployeeId}.";
                }
                else
                {
                    ViewBag.Error = "Failed to grant admin access.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error giving admin access to employee ID {EmployeeId}", SelectedEmployee.SelectedEmployeeId);
                ViewBag.Error = "An error occurred while trying to give admin access.";
            }

            return RedirectToAction("Index", "SurveyReport");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToSurvey(SurveyInviteModel newEmployeeAdded)
        {
            try
            {


                var newEmployee = new AddEmployee
                {
                    EmployeeFirstName = newEmployeeAdded.EmployeeFirstName,
                    EmployeeLastName = newEmployeeAdded.EmployeeLastName,
                    EmployeeEmail = newEmployeeAdded.EmployeeEmail,
                    DepartmentId = newEmployeeAdded.SelectedDepartmentId

                };

                bool success = await _employeeFetch.CreateEmployee(newEmployee);

                if (success)
                {
                    ViewBag.Message = $"Employee {newEmployeeAdded.EmployeeFirstName} {newEmployeeAdded.EmployeeLastName} added successfully to the survey.";
                }
                else
                {
                    ViewBag.Error = "Failed to add the employee to the survey.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding employee ID {firstName} {lastName} to survey", newEmployeeAdded.EmployeeFirstName, newEmployeeAdded.EmployeeLastName);
                ViewBag.Error = "An error occurred while trying to add the employee to the survey.";
            }
            return RedirectToAction("Index", "SurveyReport");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentSurvey()
        {
            var employeeResponces = await _fetchSurveyResults.GetAllSurveyResponseAsync();
            var employeeComments = await _fetchSurveyResults.GetAllSurveyCommentsAsync();
            var allDepartments = await _deparmentFetch.GetAllDepartmentsAsync();

            var averageEmpQenScore = await _fetchSurveyResults.EmployeeQuestionAverage();



            var averageEmployeeDepartmentScore = await _fetchSurveyResults.EmployeeDepartmentQuestionAverage();

            var viewModel = new SurveyResults
            {
                Departments = allDepartments,
                SurveyComments = employeeComments,

                EmployeeQuestionAverages = averageEmpQenScore,

                EmployeeDepartmentSurveyResults = averageEmployeeDepartmentScore



            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSession(int Id)
        {
            var deleted = await _fetchSurveyResults.DeleteSurveySessionsAsync(Id);

            if (deleted)
            {
                ViewBag.Message = $"Survey session {Id} deleted successfully.";
            }
            else
            {
                ViewBag.Error = $"Failed to delete survey session {Id}.";
               
            }

            return RedirectToAction("DepartmentSurvey", "SurveyReport");

        }
    }   
}
