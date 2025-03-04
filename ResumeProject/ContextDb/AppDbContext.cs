using ResumeProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.ContextDb;

public class AppDbContext: IdentityDbContext<ApplicationUser> // IdentityDbContext creates a tables for managing the user data in ApplicationUser
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserComment> UserComment { get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}