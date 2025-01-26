using System;
using Microsoft.EntityFrameworkCore;
using ResumeProject.ContextDb;
using ResumeProject.Models;

namespace ResumeProject.Service;

public class UserService: IService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Get all Users
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Get User by Id
    public async Task<User?> GetUserByIdAsync(int userid)
    {
        return await _context.Users.FindAsync(userid);
    }

    // Get user with their UserAccount (one-to-one relationship)
    public async Task<User?> GetUserWithAccountAsync(int userid)
    {   
        return await _context.Users.Include(u => u.UserAccount).FirstOrDefaultAsync(u => u.UserId == userid);
    }

    // Add a new user
    public async Task AdduserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // Update an existiong user
    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    // Delete a user
    public async Task DeleteUserAsync(int userid)
    {
        var user = await _context.Users.FindAsync(userid);

        if(user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
