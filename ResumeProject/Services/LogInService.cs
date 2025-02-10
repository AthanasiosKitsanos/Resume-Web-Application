using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class LogInService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogInService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LogInUserAsync(LogInDTO input)
    {
        var result = await _signInManager.PasswordSignInAsync(input.Email!, input.Password!, input.RememberMe, lockoutOnFailure: false);

        return result;
    }
}
