using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;

namespace ResumeProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly LogOutService _logOutService;
        private readonly LogInService _logInService;

        public LogoutModel(LogOutService logOutService, LogInService logInService)
        {
            _logOutService = logOutService;
            _logInService = logInService;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _logInService.IsLoggedInAsync();

            if(result)
            {
                await _logOutService.LogOutUserAsync();    
            }
            
            return RedirectToPage("/");
        }
    }
}
