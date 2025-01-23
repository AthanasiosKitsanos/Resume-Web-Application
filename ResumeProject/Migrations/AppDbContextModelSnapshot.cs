using System;
using ResumeProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ResumeProject.Migrations;

[DbContext(typeof(AppDbContext))]
public class AppDbContextModelSnapshot: ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

        // Future add convertion the User class and UserAccount class 
        // to migrate in the database
    }
}
