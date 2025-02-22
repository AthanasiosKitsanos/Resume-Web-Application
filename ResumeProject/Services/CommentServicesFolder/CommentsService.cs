using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> CreateCommentsAsync(string newComment)
    {
        var userId = _userManager.GetUserId(_signInManager.Context.User);

        if(userId is null)
        {
            return false;
        }

        var comment = new Comment
        {
            CommentText = newComment,
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

    public async Task<List<CommentDTO>> GetAllCommentsAsync()
    {
        return await _dbcontext.Comments.Include(c => c.User)
                .Select(c => new CommentDTO
                {
                    Text = c.CommentText,
                    FirstName = c.User.FirstName!,
                    LastName = c.User.LastName!,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();
    }
}
