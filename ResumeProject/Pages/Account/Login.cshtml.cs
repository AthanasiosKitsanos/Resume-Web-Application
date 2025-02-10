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
        public LogInDTO Input { get; set; } = new LogInDTO();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _logInService.LogInUserAsync(Input);

            if(result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return Page();
        }
    }
}
