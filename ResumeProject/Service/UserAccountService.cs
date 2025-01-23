using System;
using ResumeProject.Data;
using ResumeProject.Models;

namespace ResumeProject.Service;

public class UserAccountService: IService
{
    private readonly AppDbContext _context;

    public UserAccountService(AppDbContext context)
    {
        _context = context;
    }

    
}
