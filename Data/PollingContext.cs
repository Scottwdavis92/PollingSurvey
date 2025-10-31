
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PollingApplication.Models;
using System.Reflection.Emit;

namespace PollingApplication.Data
{
    public class PollingContext : IdentityDbContext<ApplicationUser>
    {
        public PollingContext(DbContextOptions<PollingContext> options) : base(options) 
        {

        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Department> Departments { get; set; }
        public DbSet<Models.SurveyResponse> SurveyResponses { get; set; }
        public DbSet<Models.ResponseScale> ResponseScales { get; set; }
        public DbSet<Models.Question> Questions { get; set; }
        public DbSet<Models.SurveySessionId> SurveySessions { get; set; }
        public DbSet<Models.SurveyComment> SurveyComments { get; set; }
        public DbSet<Models.Audit> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
            
           builder.Entity<ApplicationUser>()
                .HasOne(a => a.Employee)
                .WithOne(e=> e.ApplicationUser)
                .HasForeignKey<Employee>(a => a.ApplicationUserId);

           builder.Entity<SurveyResponse>()
                .HasOne(sr => sr.SurveySession)
                .WithMany(ss => ss.SurveyResponses)
                .HasForeignKey(sr => sr.SurveySessionId)
                .OnDelete(DeleteBehavior.Cascade); 
           
           builder.Entity<SurveySessionId>()
                .HasAlternateKey(ss => ss.SurveyGuid);

           builder.Entity<SurveyComment>()
                .HasOne(sc => sc.SurveySession) 
                .WithMany() 
                .OnDelete(DeleteBehavior.Restrict);

           builder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Employee)
                .WithMany() 
                .HasForeignKey(sr => sr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SurveySessionId>()
                .HasOne(ss => ss.Employee)
                .WithMany() 
                .HasForeignKey(ss => ss.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
