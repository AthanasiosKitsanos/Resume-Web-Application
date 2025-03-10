using System;

namespace ResumeProject.Settings;

public class UserSettings
{
    public string? UserName { get; set; }
    public string? Email { get; set;}
    public string? FirstName { get; set;}
    public string? LastName { get; set; }

    public string? Password { get; set; }

    public DateTime DateOfBirth { get; set; }
}
