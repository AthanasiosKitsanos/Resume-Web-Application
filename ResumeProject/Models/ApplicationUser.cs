using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResumeProject.Models;

public class ApplicationUser: IdentityUser
{   
    [Required]
    public string? FistName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; } 

    public int Age { get; set; }
}
