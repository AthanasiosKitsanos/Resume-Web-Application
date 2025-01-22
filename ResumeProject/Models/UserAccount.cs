using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class UserAccount
{
    public string? UserName { get; set; }
    public string? UserPassword { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    public int Age 
    { 
        get
        {
            return Age;
        }
        set
        {
            value = DateOfBirth.Year - DateTime.Now.Year; 
            Age = value;
        }    
    }

    [EmailAddress]
    public string? Email { get; set; }
}
