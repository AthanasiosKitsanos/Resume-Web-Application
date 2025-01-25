using Microsoft.EntityFrameworkCore;
using ResumeProject.Models;
using ResumeProject.Data;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UserAccounts") ?? "Data Source=ResumaAppDB.db";
builder.Services.AddSqlite<AppDbContext>(connectionString);

var app = builder.Build();

app.Run();
