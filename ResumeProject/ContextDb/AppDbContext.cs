using ResumeProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.ContextDb;

public class AppDbContext: IdentityDbContext<ApplicationUser> // It creates a DbSet of the ApplicationUser automaticaly
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=ResumeProject.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
