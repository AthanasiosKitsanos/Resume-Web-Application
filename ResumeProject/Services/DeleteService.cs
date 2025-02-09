using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class DeleteService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> DeleteUserAsync()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);

        if(user is null)
        {
            return false;
        }

        var result = await _userManager.DeleteAsync(user);

        if(result.Succeeded)
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        return false;
    }
}
