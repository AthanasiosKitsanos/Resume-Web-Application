using System;
using ResumeProject.ContextDb;
using ResumeProject.Models;

namespace ResumeProject.DbQueries;

public class RegisterUserQuery
{
    private readonly AppDbContext _dbcontext;
    private readonly ApplicationUser _user;

    public bool Succeeded { get; set; } = true;

    public RegisterUserQuery(AppDbContext dbcontext, ApplicationUser user)
    {
        _dbcontext = dbcontext;
        _user = user;
    }

    public async Task<bool> DbRegisterUserAsync(ApplicationUser user)
    {
        if(user is not null)
        {
            await _dbcontext.Users.AddAsync(user);
            return true;
        }
        
        return false;
    } 
}
