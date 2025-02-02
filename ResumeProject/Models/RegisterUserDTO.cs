using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class RegisterUserDTO
{
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(8, ErrorMessage = "The password must be atleast 8 character long")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
}
