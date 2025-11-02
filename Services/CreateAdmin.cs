using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PollingApplication.Data;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    //this is not recommended way to add new users/roles but is done for simplicity in this project
    public class CreateAdmin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PollingContext _dbContext;

        public CreateAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, PollingContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<bool> AddAdminRoleAndUserAsync(Employee employee)
        {

            // Seed the single "Admin" role
            string roleName = "Admin";
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Seed an admin user and assign the "Admin" role
            var adminUser = await _userManager.FindByEmailAsync(employee.Email);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = employee.Email,
                    Email = employee.Email,
                    EmailConfirmed = true,
                };
                var createAdmin = await _userManager.CreateAsync(adminUser, "Password123!");
                if (createAdmin.Succeeded)
                {

                    employee.ApplicationUserId = adminUser.Id;

                    await _dbContext.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(adminUser, roleName);
                }

               
            }

            return true;
        }
    }
}
