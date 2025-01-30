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

    public async Task<string> LoginUserAsync(string email, string password, bool rememberMe = false)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return null!;
        }

        var roles = _userManager.GetRolesAsync(user);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var jwtSettings = SharedSettings.GetJwtSettings();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Log out user
    public async Task LogOutUser()
    {
        await _signInManager.SignOutAsync();
    }
}
