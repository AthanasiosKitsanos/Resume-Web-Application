using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class User
{
    public int UserId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public UserAccount? UserAccount{ get; set; }
}
