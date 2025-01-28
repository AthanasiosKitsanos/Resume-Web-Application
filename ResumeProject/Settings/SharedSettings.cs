using System;

namespace ResumeProject.Settings;

public class SharedSettings
{
    public static JwtSettings GetJwtSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        return jwtSettings!;
    }

    public static UserSettings GetUserSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var userSettings = configuration.GetSection("UserSettings").Get<UserSettings>();

        return userSettings!;
    }
}
