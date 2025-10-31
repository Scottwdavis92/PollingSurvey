using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace PollingApplication.Startup
{
    public static class ScopeDependencies
    {
        public static void AddScopeDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<PollingApplication.Services.SurveyInvite>();
            builder.Services.AddScoped<PollingApplication.Services.EmployeeFetch>();
            builder.Services.AddScoped<PollingApplication.Services.Mailer>();
            builder.Services.AddScoped<PollingApplication.Services.QuestionFetch>();
            builder.Services.AddScoped<PollingApplication.Services.SubmittedSurvey>();
            builder.Services.AddScoped<PollingApplication.Services.FetchSurveyResults>();
        }
    }
}
