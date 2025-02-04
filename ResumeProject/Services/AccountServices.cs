using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ResumeProject.Settings;
using System.Linq;
using ResumeProject.ContextDb;


namespace ResumeProject.Services;

public class AccountServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly AppDbContext _context;
    public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _roleManager = roleManager;
        _context = context;
    }

    // Register a new user
    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
        user.Age = DateTime.Now.Year - user.DateOfBirth.Year;

        if(DateTime.Now.Date < user.DateOfBirth.Date.AddYears(user.Age))
        {
            user.Age--;
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
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

    // Update info Methods
    public IQueryable<object> GetCurrentUserInfo()
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        return _context.Users.Where(u => u.Id == userId).Select(u => new 
        {   
            u.Id,
            u.Email,
            u.FirstName,
            u.LastName,
            u.PhoneNumber,
            u.DateOfBirth,
            u.Age
            
        });
    }

    public IQueryable<object> GetAllUsers()
    {
        return _context.Users.Select(u =>  new 
        {
            u.Id,
            u.Email,
            u.FirstName,
            u.LastName,
            u.PhoneNumber,
            u.DateOfBirth,
            u.Age
        });
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

     // Delete current user method
    public async Task<bool> DeleteAccountAsync()
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
