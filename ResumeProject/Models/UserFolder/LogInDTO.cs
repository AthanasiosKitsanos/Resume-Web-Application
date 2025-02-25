using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class LogInDTO
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}
