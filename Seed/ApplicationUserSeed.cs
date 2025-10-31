using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PollingApplication.Data;
using PollingApplication.Models;
using System;
using System.Threading.Tasks;

namespace PollingApplication.Seed
{
    public static class ApplicationUserSeed
    {


        public static async Task SeedAdminRoleAndUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = serviceProvider.GetRequiredService<PollingContext>();


            // Seed the single "Admin" role
            string roleName = "Admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Seed an admin user and assign the "Admin" role
            var adminUser = await userManager.FindByEmailAsync("Scottwdavis92@gmail.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "Scottwdavis92@gmail.com",
                    Email = "Scottwdavis92@gmail.com",
                    EmailConfirmed = true,
                };
                var createAdmin = await userManager.CreateAsync(adminUser, "Password123!");
                if (createAdmin.Succeeded)
                {
                    var employee = new Employee
                    {
                        FirstName = "Scott",
                        LastName = "Davis",
                        Email = "Scottwdavis92@gmail.com",
                        Departmentid = 1,
                        ApplicationUserId = adminUser.Id

                    };
                    dbContext.Employees.Add(employee);
                    await dbContext.SaveChangesAsync();
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}
    
