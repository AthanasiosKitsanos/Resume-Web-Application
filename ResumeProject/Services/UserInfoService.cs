using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        
        var userInfoList = await _dbContext.Users.Join(_dbContext.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new {user, userRole})
            .Join(_dbContext.Roles, userWithRole => userWithRole.userRole.RoleId, role => role.Id, (userWithRole, role) => new {userWithRole.user, role})
            .Where(userWithRole => userWithRole.role.Name == "User")
            .Select(u => new UserInfoDTO
            {
                FirstName = u.user.FirstName!,
                LastName = u.user.LastName!,
                Email = u.user.Email!,
                PhoneNumber = u.user.PhoneNumber!,
                Age = u.user.Age,
                DateOfBirth = u.user.DateOfBirth
            }).ToListAsync();

        return userInfoList;
    }
}