using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class LogOutService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenService _jwtTokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public LogOutService(SignInManager<ApplicationUser> signInManager, JwtTokenService jwtTokenService, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }

    public async Task LogOutUserAsync()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);
        if(user is not null)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
        }

        _signInManager.Context.Response.Cookies.Delete("access_token");
        _signInManager.Context.Response.Cookies.Delete("refresh_token");

        await _signInManager.SignOutAsync();
    }
}
