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

    
    // Update an account
    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        var existingUser = await _userManager.FindByIdAsync(user.Id);

        if(existingUser is null)
        {
            return IdentityResult.Failed();
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.UserName = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;

        return await _userManager.UpdateAsync(existingUser);
    }

    // Delete an account
    public async Task<IdentityResult> DeleteUserAsync(string userid)
    {
        var user = await _userManager.FindByIdAsync(userid);

        if(user is null)
        {
            return IdentityResult.Failed();
        }

        return await _userManager.DeleteAsync(user);
    }
}
