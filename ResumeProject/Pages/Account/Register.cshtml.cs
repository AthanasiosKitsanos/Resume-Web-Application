using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Models;
using ResumeProject.Services;
using Microsoft.AspNetCore.Identity;

namespace ResumeProject.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AccountServices? _accountServices;

        public RegisterModel(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [BindProperty]
        public RegisterUserDTO  RegisterDTO{ get; set; } = new();
        public void OnGet() {}

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = RegisterDTO.Email,
                Email = RegisterDTO.Email,
                FirstName = RegisterDTO.FirstName,
                LastName = RegisterDTO.LastName,
                DateOfBirth = RegisterDTO.DateOfBirth
            };
    
            var result = await _accountServices.RegisterUserAsync(user, RegisterDTO.Password!);

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
