using System;

namespace ResumeProject.Models;

public class RegisterUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool rememberMe { get; set; }
}
