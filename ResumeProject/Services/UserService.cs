using Microsoft.AspNetCore.Identity;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class UserService: IUserService
{
    private readonly UserManager<ApplicationUser> _userManager; // Used for creating, managing and validating users

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password); // We use CreateAsync to create a new user.
        return result;
    }
}
