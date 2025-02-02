using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Models;
using ResumeProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResumeProject.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AccountServices _accountServices;

        public RegisterModel(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        
        [BindProperty]
        public RegisterUserDTO RegisterDto { get; set; } = new RegisterUserDTO();

        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                foreach(var ModelStateEntry in ModelState.Values)
                {
                    foreach(var error in ModelStateEntry.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
                
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = RegisterDto.Email,
                Email = RegisterDto.Email,
                FirstName = RegisterDto.FirstName,
                LastName = RegisterDto.LastName,
                DateOfBirth = RegisterDto.DateOfBirth
            };
    
            var result = await _accountServices.RegisterUserAsync(user, RegisterDto.Password!);

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
