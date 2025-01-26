using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;
using ResumeProject.ContextDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using ResumeProject.Services;


namespace ResumeProject;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the containers
        builder.Services.AddRazorPages(); // To use Razor pages
        builder.Services.AddControllers(); // To use controllers
        builder.Services.AddServerSideBlazor(); // To use Blazor in the server's side

        // Add Identity and configure JWT authentication
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "YourIssuer",
                ValidAudience = "YourAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"))
            };   
        });

        builder.Services.AddScoped<IUserService, UserService>();
        var app = builder.Build();

        app.MapRazorPages();
        app.MapBlazorHub();

        app.Run();
    }
}