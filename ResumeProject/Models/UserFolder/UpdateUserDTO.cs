using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeProject.Models;

public class UpdateUserDTO
{
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(8, ErrorMessage = "The password must be atleast 8 character long")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }
}
