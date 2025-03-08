using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO input)
    {
        var user = new ApplicationUser
        {
            UserName = input.Email,
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            DateOfBirth = input.DateOfBirth,
            PhoneNumber = input.PhoneNumber
        };

        user.Age = DateTime.Now.Year - user.DateOfBirth.Year;

        if(DateTime.Now.Date < user.DateOfBirth.Date.AddYears(user.Age))
        {
            user.Age--;
        }

        var result = await _userManager.CreateAsync(user, input.Password);
        if(result.Succeeded)
        {
            if(!await _roleManager.RoleExistsAsync("User"))
            {
               await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");
        }

        return result;
    }
}
