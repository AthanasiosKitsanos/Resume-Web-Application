using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;

namespace ResumeProject.Pages.Account
{
    public class UpdateModel : PageModel
    {
        private readonly UpdateService _updateService;

        public UpdateModel(UpdateService updateService)
        {
            _updateService = updateService;
        }
        public void OnGet()
        {

        }

        [BindProperty]
        public UpdateUserDTO UpdateDto { get; set; } = new UpdateUserDTO();

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

            var result = await _updateService.UpdateUserInfoAsync(UpdateDto);

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
