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

        public SurveyReportController(ILogger<SurveyReportController> logger, SurveyInvite surveyInvite, EmployeeFetch employeeFetch, Mailer mailer)
        {
            _logger = logger;
            _mailer = mailer;
            _surveyInvite = surveyInvite;
            _employeeFetch = employeeFetch;
        }

        public IActionResult Index()
        {
            return View();
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

            return View("Index");
        }

    }
}
