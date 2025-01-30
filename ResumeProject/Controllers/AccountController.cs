using System;
using Microsoft.AspNetCore.Mvc;
using ResumeProject.Models;
using ResumeProject.Services;

namespace ResumeProject.Controllers;

// This controller manages the http requests fot logging in, registration and logging out

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    private readonly AccountServices _accountServices;

    public AccountController(AccountServices accountServices)
    {
        _accountServices = accountServices;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = DateTime.Now
        };

        var result = await _accountServices.RegisterUserAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Account has been created!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInDTO model)
    {
        var token = await _accountServices.LoginUserAsync(model.Email!, model.Password!, model.RememberMe);

        if(token is null)
        {
            return Unauthorized("Invalide Username or Password, please try again");
        }

        return Ok(new {Token = token});
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountServices.LogOutUser();

        return Ok("Logged out succesfully");
    }
}
