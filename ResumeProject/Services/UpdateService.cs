using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class UpdateService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> UpdateUserInfoAsync(UpdateUserDTO input)
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);

        if(user is null)
        {
            return false;
        }

        if(!string.IsNullOrEmpty(input.Email))
        {
            user.Email = input.Email;
            user.UserName = input.Email;
        }

        if(!string.IsNullOrEmpty(input.FirstName))
        {
            user.FirstName = input.FirstName;
        }

        if(!string.IsNullOrEmpty(input.LastName))
        {
            user.LastName = input.LastName;
        }

        if(!string.IsNullOrEmpty(input.PhoneNumber))
        {
            user.PhoneNumber = input.PhoneNumber;
        }

        var result = await _userManager.UpdateAsync(user);
        if(result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
        }

        return true;
    }
}
