using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PollingApplication.Models;
using PollingApplication.Services;
using PollingApplication.ViewModels;
using System.Diagnostics;

namespace PollingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SurveyInvite _surveyInvite;
        private readonly QuestionFetch _questionfetch;
        private readonly SubmittedSurvey _submittedSurvey;

        public HomeController(ILogger<HomeController> logger, SurveyInvite surveyInvite, QuestionFetch questionFetch, SubmittedSurvey submittedSurvey)
        {
            _logger = logger;
            _surveyInvite = surveyInvite;
            _questionfetch = questionFetch;
            _submittedSurvey = submittedSurvey;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        public IActionResult SurveyCompleted()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Survey(string token)
        {
            if(token == null)
            {
                return BadRequest("Survey ID is required.");
            }

            var invitation = await _surveyInvite.GetSurveyInviteByGuidAsync(Guid.Parse(token));

            if (invitation == null || invitation.Completed)
            {
                return NotFound("Survey invitation not found.");
            }

            var genQuestion = await _questionfetch.GetGeneralQuestionsAsync();
            var depQuestion = await _questionfetch.GetDepartmentQuestionsAsync(invitation.Employee.Departmentid);

            var responseScale = await _questionfetch.GetReponseScale();

            var scale = responseScale.Select(rs => new SelectListItem
            {
                Value = rs.ScaleValue.ToString(),
                Text = rs.ScaleDescription

            }).ToList();

            var viewModel = new SurveyerModel
            {
                genQuestions = genQuestion,
                deptQuestions = depQuestion,
                ScaleValues = scale,
                SurveyGuid = token,
                EmployeeId = invitation.EmployeeId

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSurvey([FromBody] SubmitSurvey model)
        {
            

            var surveyId = await _surveyInvite.MarkSurveyAsCompletedAsync(Guid.Parse(model.SurveyGuid));

            foreach (var surRes in model.surveyResponses)
            {
                surRes.SurveySessionId = surveyId!.Id;
                await _submittedSurvey.SaveSubmittedResponseSurveyAsync(surRes);
            }

            if (!string.IsNullOrWhiteSpace(model.surveyComments))
            {
                await _submittedSurvey.SaveSubmittedCommentAsync(model.surveyComments!, surveyId!.Id, surveyId!.EmployeeId);
            }
            
            return Json(new
            {
                success = true,
                message = "Thank you for your feedback!",
                redirectUrl = Url.Action("SurveyCompleted", "Home") // Generate the URL to the completion view
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
