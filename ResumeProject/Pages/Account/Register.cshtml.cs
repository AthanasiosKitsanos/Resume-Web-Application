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
        private readonly RegisterService _registerService;

        public RegisterModel(RegisterService registerService)
        {
            _registerService = registerService;
        }

        
        [BindProperty]
        public RegisterUserDTO Input { get; set; } = new RegisterUserDTO();

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
    
            var result = await _registerService.RegisterUserAsync(Input);

            if(result is null)
            {
                return null!;
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
