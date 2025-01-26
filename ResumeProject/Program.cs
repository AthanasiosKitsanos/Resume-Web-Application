using ResumeProject.Models;
using ResumeProject.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AppDbContext>(); // We add to the Dependency Injection container the AppDbContext
        builder.Services.AddServerSideBlazor();
        builder.Services.AddRazorPages();
        builder.Services.AddRazorComponents();

        var app = builder.Build();

        

        app.Run();
    }
}