using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class LogOutService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogOutService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task LogOutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
