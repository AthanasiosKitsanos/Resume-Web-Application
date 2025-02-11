using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;

namespace ResumeProject.Pages.Account
{
    public class UpdateModel : PageModel
    {
        private readonly UpdateService _updateService;
        private readonly LogInService _logInService;

        public UpdateModel(UpdateService updateService, LogInService logInService)
        {
            _updateService = updateService;
            _logInService = logInService;
        }

        [BindProperty]
        public UpdateUserDTO Input { get; set; } = new UpdateUserDTO();

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

            var result = await _updateService.UpdateUserInfoAsync(Input);

            if(result)
            {
                return RedirectToPage("/Account/UpdateSuccess");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update user information");
                return Page();
            }
        }
    }
}
