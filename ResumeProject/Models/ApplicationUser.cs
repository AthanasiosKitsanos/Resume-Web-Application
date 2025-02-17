using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResumeProject.Models;

public class ApplicationUser: IdentityUser // IdentiryUser already provides the necessary properties for authentication like UserName, Email, PasswordHash, etc.
{   
    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; } 

    public int Age { get; set; }
}
