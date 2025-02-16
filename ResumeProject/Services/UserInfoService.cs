using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.Services;

public class UserInfoService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _dbContext;

    public UserInfoService(UserManager<ApplicationUser> userManager, AppDbContext dbContext, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _signInManager = signInManager;
    }

    // Returns the current user that is logged in information
    // public async Task<ApplicationUser> GetLoggedInUserInfoAsync(ClaimsPrincipal principal)
    // {
    //     return await _userManager.GetUserAsync(principal);
    // }

    public async Task<List<UserInfoDTO>> GetLoggedInUserInfoAsync()
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

         var userInfo = await _dbContext.Users.Where(u => u.Id == userId)
                                              .Select(u => new UserInfoDTO
                                              {
                                                FirstName = u.FirstName!,
                                                LastName = u.LastName!,
                                                Email = u.Email!,
                                                PhoneNumber = u.PhoneNumber!,
                                                Age = u.Age,
                                                DateOfBirth = u.DateOfBirth
                                               }).ToListAsync();

        return userInfo;
    }

    // returns Information about all users
    // This method will be called, only by the Admins
    public async Task<List<UserInfoDTO>> GetAllUserInfo()
    {
        var userInfoList  = await _dbContext.Users.Select(u => new UserInfoDTO
        {
            FirstName = u.FirstName!,
            LastName = u.LastName!,
            Email = u.Email!,
            PhoneNumber = u.PhoneNumber!,
            Age = u.Age,
            DateOfBirth = u.DateOfBirth
        }).ToListAsync();

        return userInfoList;
    }
}
