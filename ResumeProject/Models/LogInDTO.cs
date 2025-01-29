using System;

namespace ResumeProject.Models;

public class LogInDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}
