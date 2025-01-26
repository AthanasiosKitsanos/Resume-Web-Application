using System;

namespace ResumeProject.Settings;

public class JwtSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SectretKey { get; set; }
}
