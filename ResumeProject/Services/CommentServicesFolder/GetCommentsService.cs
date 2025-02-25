using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.Services;

public class GetCommentsService
{
    private readonly AppDbContext _dbcontext;

    public GetCommentsService(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<CommentDTO>> GetAllCommentsAsync()
    {
        return await _dbcontext.Comments.Join(_dbcontext.UserComment, comment => comment.Id, commentUser => commentUser.CommentId, (comment, commentUser) => new {comment, commentUser})
                        .Join(_dbcontext.Users, commentWithUser => commentWithUser.commentUser.UserId, user => user.Id, (commentWithUser, user) => new {commentWithUser.comment, user})
                        .Select(u => new CommentDTO
                        {
                            Text = u.comment.CommentText,
                            FirstName = u.user.FirstName!,
                            LastName = u.user.LastName!,
                            CreatedAt = u.comment.CreatedAt
                        }).ToListAsync();
                        
        // return await _dbcontext.Comments.Include(c => c.User)
        //         .Select(c => new CommentDTO
        //         {
        //             Text = c.CommentText,
        //             FirstName = c.User.FirstName!,
        //             LastName = c.User.LastName!,
        //             CreatedAt = c.CreatedAt
        //         }).ToListAsync();
    }
}
