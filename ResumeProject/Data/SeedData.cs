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

        // Add default admin user
        var defaultUser = await userManager.FindByEmailAsync("kitsanos.dev@gmail.com");

        if(defaultUser is null)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            var userSettings = configuration.GetSection("UserSettings").Get<UserSettings>();

            var admin = new ApplicationUser
            {
                UserName = userSettings!.UserName,
                Email = userSettings.Email,
                FirstName = userSettings.FirstName,
                LastName = userSettings.LastName,
            };

            await userManager.CreateAsync(admin, "Zizoulini11524!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
