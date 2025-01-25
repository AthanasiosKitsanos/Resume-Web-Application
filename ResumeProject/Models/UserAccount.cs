using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class UserAccount
{
    [Required]
    public int UserAccountId { get; set; }

    [Required]
    [MaxLength(40)]
    public string? UserName { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])(?=.{8,}).*$")]
    public string? UserPassword { get; set; }

    [Required]
    public string? Role => IsAdmin == true ? "Admin" : "User";

    public bool? IsAdmin { get; set; } = false;

    public User? User { get; set; }

    public int UserId { get; set; }
}
