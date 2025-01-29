using System;
using ResumeProject.Models;
using Microsoft.AspNetCore.Identity;

namespace ResumeProject.Services;

public class AccountServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Register a new user
    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
        user.Age = DateTime.Now.Year - user.DateOfBirth.Year;

        if(DateTime.Now.Date < user.DateOfBirth.Date.AddYears(user.Age))
        {
            user.Age--;
        }
        
        var result = await _userManager.CreateAsync(user, password); // We use CreateAsync to create a new user.
        await _userManager.AddToRoleAsync(user, "User");
        
        return result;
    }

    // Log In user

    public async Task<SignInResult> LoginUserAsync(string email, string password, bool rememberMe = false)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.PasswordSignInAsync(user.UserName!, password, rememberMe, lockoutOnFailure: false);
    }

    // Log out user
    public async Task LogOutUser()
    {
        await _signInManager.SignOutAsync();
    }
}
