using Microsoft.EntityFrameworkCore;
using PollingApplication.Data;
using PollingApplication.Models;
using Microsoft.AspNetCore.Identity;


namespace PollingApplication.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext
            builder.Services.AddDbContext<PollingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PollingContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Point the login path to your new MVC controller action
                options.LoginPath = "/Account/Login";
            });

            builder.Services.AddScoped<PollingApplication.Services.SurveyInvite>();
            builder.Services.AddScoped<PollingApplication.Services.EmployeeFetch>();
            builder.Services.AddScoped<PollingApplication.Services.Mailer>();
            builder.Services.AddScoped<PollingApplication.Services.QuestionFetch>();
        }
    }
}
