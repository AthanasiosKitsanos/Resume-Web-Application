using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;
using ResumeProject.ContextDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using ResumeProject.Services;
using ResumeProject.Data;
using ResumeProject.Settings;
using Microsoft.Extensions.Options;
using ResumeProject.Pages.Comments;

namespace ResumeProject;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the containers
        builder.Services.AddRazorPages(); // To use Razor pages // To use controllers

        // Add Identity and configure JWT authentication
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=ResumeProject.db"));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        builder.Services.ConfigureApplicationCookie(options => 
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });
        
        builder.Services.AddScoped<LogInService>();
        builder.Services.AddScoped<DeleteService>();
        builder.Services.AddScoped<LogOutService>();
        builder.Services.AddScoped<RegisterService>();
        builder.Services.AddScoped<UpdateService>();
        builder.Services.AddScoped<UserInfoService>();
        builder.Services.AddScoped<CreateCommentsService>();
        builder.Services.AddScoped<GetCommentsService>();
        
        var app = builder.Build();

        if(!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages();

        app.Run();
    }
}