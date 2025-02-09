using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class RegisterService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public RegisterService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
        user.Age = DateTime.Now.Year - user.DateOfBirth.Year;

        if(DateTime.Now.Date < user.DateOfBirth.Date.AddYears(user.Age))
        {
            user.Age--;
        }

        if(!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        var result = await _userManager.CreateAsync(user, password);

        await _userManager.AddToRoleAsync(user, "User");

        return result;
    }
}
