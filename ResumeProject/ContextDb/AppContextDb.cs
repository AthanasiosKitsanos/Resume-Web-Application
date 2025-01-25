using System;
using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.ContextDb;

public class AppDbContext: DbContext
{
    public DbSet<User> Users { get; set; } // Represents the table of the Users in the database
    public DbSet<UserAccount> UserAccounts { get; set; } // Represents the table of the UserAccounts in the database

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Configures the SQLite database in the root of the project
    {
        optionsBuilder.UseSqlite("Data Source=ResumeProject.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Defines a one-to-one relationship between User and UserAccount, using UserId as the foreign key.
    {
        modelBuilder.Entity<User>()
                    .HasOne(u => u.UserAccount)
                    .WithOne(ua => ua.User)
                    .HasForeignKey<UserAccount>(ua => ua.UserId)
                    .IsRequired();
    }
}
