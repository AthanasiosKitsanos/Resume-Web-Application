using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class UserAccount
{
    public int UserAccountId { get; set; }
    public string? UserName { get; set; }
    public string? UserPassword { get; set; }

    public string? Role => IsAdmin == true ? "Admin" : "User";

    public bool? IsAdmin { get; set; } = false;

    public User? User { get; set; }

    public int UserId { get; set; }
}
