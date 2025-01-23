using System;
using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>()
                    .HasOne(ua => ua.User) // UserAccount has one User (us stands for UserAccount)
                    .WithOne(u => u.UserAccount) // User has one UserAccount (u stands for User)
                    .HasForeignKey<UserAccount>(ua => ua.UserId) // Foreign Key in UserAccont class in the property UserId
                    .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<UserAccount>().HasData
        (
            new UserAccount 
            {
                UserAccountId = 2001,
                UserName = "kitsanos.dev@gmail.com",
                UserPassword = "mM!78695478mM!",
                IsAdmin = true,
                UserId = 1001 // This way we link the UserId int he User class with the UserId in the UserAccount class
            }    
        );

        modelBuilder.Entity<User>().HasData
        (
            new User 
            {
                UserId = 1001,
                FirstName = "Athanasios",
                LastName = "Kitsanos",
                DateOfBirth = new(1993, 05, 04),
                Email = "kitsanos.dev@gmail.com"
            }
        );
    }
}
