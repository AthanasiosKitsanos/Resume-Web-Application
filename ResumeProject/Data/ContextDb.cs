using System;
using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.ContextDb;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; } = null!;
}
