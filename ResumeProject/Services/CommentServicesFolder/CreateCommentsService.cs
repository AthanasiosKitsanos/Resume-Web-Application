using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.Services;

public class CreateCommentsService
{
    private readonly AppDbContext _dbcontext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateCommentsService(AppDbContext dbcontext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _dbcontext = dbcontext;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> CreateCommentsServicesAsync(CommentDTO input)
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        if(userId is null)
        {
            return false;
        }

        var comment = new Comment
        {
            CommentText = input.Text,
            UserId = userId
        };

        _dbcontext.Comments.Add(comment);
        await _dbcontext.SaveChangesAsync();

        var userComment = new UserComment
        {
            UserId = userId,
            CommentId = comment.Id
        };

        _dbcontext.UserComment.Add(userComment);
        await _dbcontext.SaveChangesAsync();

        return true;
    }

   
}
