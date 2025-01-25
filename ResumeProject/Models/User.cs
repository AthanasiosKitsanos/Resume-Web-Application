using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeProject.Models;

public class User
{
    public int UserId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [Column(TypeName = "DATE")]
    public DateTime DateOfBirth { get; set; }

    public int Age { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public UserAccount? UserAccount{ get; set; }
}
