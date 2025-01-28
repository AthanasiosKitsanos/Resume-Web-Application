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

namespace ResumeProject;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the containers
        builder.Services.AddRazorPages(); // To use Razor pages
        builder.Services.AddControllers(); // To use controllers
        builder.Services.AddServerSideBlazor(); // To use Blazor in server's side

        // Add Identity and configure JWT authentication
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=ResumeProject.db"));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        var jwtSettings = SharedSettings.GetJwtSettings();

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
                ValidIssuer = jwtSettings!.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SectretKey!))
            };   
        });

        builder.Services.AddScoped<IUserService, UserService>();
        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapRazorPages();
        app.MapBlazorHub();

        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedData.Initialize(services, userManager);
        }

        app.Run();
    }
}