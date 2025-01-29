using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ResumeProject.Settings;

namespace ResumeProject.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if(user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return null!; // Invalid credantials
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var jwtSeetings = SharedSettings.GetJwtSettings();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSeetings.SecretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: jwtSeetings.Issuer,
            audience: jwtSeetings.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
