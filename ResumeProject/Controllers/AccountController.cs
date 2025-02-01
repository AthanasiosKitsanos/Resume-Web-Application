using System;
using Microsoft.AspNetCore.Mvc;
using ResumeProject.Models;
using ResumeProject.Services;

namespace ResumeProject.Controllers;

// This controller manages the http requests fot logging in, registration and logging out

[ApiController]
[Route("api/[controller]")]
public class AccountController: Controller
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

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountServices.LogOutUser();

        return RedirectToAction("Index", "Home");
    }

    public bool IsLoggedIn()
    {   
        if(_accountServices.IsLoggedIn())
        {
            return true;
        }

        return false;
    }

    public async Task<IActionResult> GetUsersName()
    {
        // Call the GetUserName method from AccountServices
        var userName = await _accountServices.GetUserName();

        // If userName is empty (meaning no user is logged in), return a 404 or other status
        if (string.IsNullOrEmpty(userName))
        {
            return NotFound("No user is logged in.");
        }

        // Return the username in the response
        return Ok(userName);
    }
}
