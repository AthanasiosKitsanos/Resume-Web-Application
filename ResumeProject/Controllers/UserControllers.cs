using System;
using Microsoft.AspNetCore.Mvc;
using ResumeProject.Models;
using ResumeProject.Services;

namespace ResumeProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserControllers: ControllerBase    
{
    private readonly UserService _userService;

    public UserControllers(UserService userService)
    {
        _userService = userService;
    }

    [HttpPut("update-info")]
    public async Task<IActionResult> Update([FromBody] UpdateUserDTO model)
    {
        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userService.UpdateUserAsync(user);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User info updated succesfully");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Account deleted succesfully");
    }
}
