using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class RegisterUserDTO
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MinLength(8, ErrorMessage = "The password must be atleast 8 character long")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set;} = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;
}
