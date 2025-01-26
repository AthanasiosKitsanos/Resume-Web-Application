using System;
using Microsoft.EntityFrameworkCore;
using ResumeProject.ContextDb;
using ResumeProject.Models;

namespace ResumeProject.Service;

public class UserAccountService
{
    private readonly AppDbContext _context;

    public UserAccountService(AppDbContext context)
    {
        _context = context;
    }

    // Get all user accounts
    public async Task<List<UserAccount>> GetAllUserAccountsAsync()
    {
        return await _context.UserAccounts.ToListAsync();
    }

    // Get user account by ID
    public async Task<UserAccount?> GetUserAccountByIdAsync(int userid)
    {
        return await _context.UserAccounts.FindAsync(userid);
    }

    // Get user account with its assiciated User
    public async Task<UserAccount?> GetAccountWithUserAsync(int userid)
    {
        return await _context.UserAccounts.Include(us => us.User).FirstOrDefaultAsync(us => us.UserId == userid);
    }

    // Add new user account
    public async Task AddUserAccountAsync(UserAccount userAccount)
    {
        _context.UserAccounts.Add(userAccount);
        await _context.SaveChangesAsync();
    }

    // Update an existing user account
    public async Task UpdateUserAccountAsync(UserAccount userAccount)
    {
        _context.UserAccounts.Update(userAccount);
        await _context.SaveChangesAsync();
    }

    // Delete a user account
    public async Task DeleteUserAccountAsync(int userid)
    {
        var userAccount = await _context.UserAccounts.FindAsync(userid);
        if(userAccount is not null)
        {
            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();
        }
    }
}
