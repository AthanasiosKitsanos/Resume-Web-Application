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
using ResumeProject.Extention;

namespace ResumeProject;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the containers
        builder.Services.AddRazorPages(); // To use Razor pages

        // Add Identity and configure JWT authentication
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        builder.Services.ConfigureApplicationCookie(options => 
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });
        
        builder.Services.AddCustomServices();

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