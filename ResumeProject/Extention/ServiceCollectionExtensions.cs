using System;
using ResumeProject.Services;

namespace ResumeProject.Extention;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<LogInService>();
        services.AddScoped<DeleteService>();
        services.AddScoped<LogOutService>();
        services.AddScoped<RegisterService>();
        services.AddScoped<UpdateService>();
        services.AddScoped<UserInfoService>();
        services.AddScoped<CreateCommentsService>();
        services.AddScoped<GetCommentsService>();

        return services;
    }
}
