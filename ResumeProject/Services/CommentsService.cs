using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using Microsoft.AspNetCore.Identity;

namespace ResumeProject.Services;

public class CommentsService
{
    private readonly AppDbContext _dbcontext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public CommentsService(AppDbContext dbcontext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _dbcontext = dbcontext;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> AddCommentsAsync(string newComment)
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        if(userId is null)
        {
            return false;
        }

        var comment = new Comment
        {
            CommentText = newComment
        };

        _dbcontext.Comments.Add(comment);
        await _dbcontext.SaveChangesAsync();

        var userComment = new UserComment
        {
            UserId = userId,
            CommentId = comment.CommentId
        };

        _dbcontext.UserComment.Add(userComment);
        await _dbcontext.SaveChangesAsync();

        return true;
    }
}
