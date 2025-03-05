using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;
using ResumeProject.Settings;

namespace ResumeProject.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Create roles if they don't exist
        string[] roleNames = { "Admin", "User" };

        foreach(var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            
            if(!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var userSettings = SharedSettings.GetUserSettings();

        // Add default admin user
        var defaultUser = await userManager.FindByEmailAsync(userSettings.Email!);

        if(defaultUser is null)
        {
            var admin = new ApplicationUser
            {
                UserName = userSettings!.UserName,
                Email = userSettings.Email,
                FirstName = userSettings.FirstName,
                LastName = userSettings.LastName,
                DateOfBirth = userSettings.DateOfBirth
            };

            admin.Age = DateTime.Now.Year - admin.DateOfBirth.Year;
            if(DateTime.Now.Date < admin.DateOfBirth.Date.AddYears(admin.Age))
            {
                admin.Age--;
            }

            await userManager.CreateAsync(admin, userSettings.Password!);
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}