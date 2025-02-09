using System;
using Microsoft.AspNetCore.Identity;
using ResumeProject.ContextDb;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class QueryService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public QueryService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    public IQueryable<object> GetCurrentUserInfo()
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        return _context.Users.Where(u => u.Id == userId).Select( u =>  new
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
        return _context.Users.Select(u => new
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

    public IQueryable<UserNameDTO> GetFirstAndLastName()
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        return _context.Users.Where(u => u.Id == userId).Select(u => new UserNameDTO
        {
            FirstName = u.FirstName,
            LastName = u.LastName
        });
    }
}
