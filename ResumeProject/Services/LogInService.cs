using System;
using ResumeProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ResumeProject.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResumeProject.Services;

public class LogInService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogInService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string> LogInUserAsync(string email, string password, bool rememberMe = false)
    {
        var result = await  IsLoggedInAsync();

        if(!result)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null!;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtSettings = SharedSettings.GetJwtSettings();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey!));
            var creds =  new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expirationDate = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return "User is already Logged In";
    }

    public async Task<bool> IsLoggedInAsync()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);

        if(user is null)
        {
            return false;
        }

        return true;
    }
}
