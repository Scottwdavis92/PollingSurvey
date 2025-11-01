using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public SurveyReportController(ILogger<SurveyReportController> logger, SurveyInvite surveyInvite, EmployeeFetch employeeFetch, Mailer mailer, FetchSurveyResults fetchSurveyResults)
        {
            _logger = logger;
            _mailer = mailer;
            _surveyInvite = surveyInvite;
            _employeeFetch = employeeFetch;
            _fetchSurveyResults = fetchSurveyResults;
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
        public async Task<IActionResult> SurveyInvite()
        {
            var employees = await _employeeFetch.GetAllEmployeesAsync();

           
            if (employees == null) {
                _logger.LogError("No employees found.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var employeeSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.FirstName} {e.LastName} ({e.Email})"
            }).ToList();

            var viewModel = new SurveyInviteModel
            {
                Employees = employeeSelectList
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

    }
}
