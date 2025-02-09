using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;

namespace ResumeProject.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly LogInService _logInService;

        public LoginModel(LogInService logInServices)
        {
            _logInService = logInServices;
        }

        [BindProperty]
        public LogInDTO LoginDto  { get; set; } = new LogInDTO();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var token = await _logInService.LogInUserAsync(LoginDto.Email!, LoginDto.Password!, LoginDto.RememberMe);

            if(token is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password, please try again");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
