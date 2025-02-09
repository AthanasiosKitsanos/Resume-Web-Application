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

    public async Task<bool> UpdateUserInfoAsync(UpdateUserDTO updateUserDto)
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);

        if(user is null)
        {
            return false;
        }

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.Email = updateUserDto.Email;
        user.UserName = updateUserDto.Email;
        user.PhoneNumber = updateUserDto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        return true;
    }
}
