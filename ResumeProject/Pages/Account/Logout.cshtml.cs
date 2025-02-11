using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;

namespace ResumeProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly LogOutService _logOutService;

        public LogoutModel(LogOutService logOutService)
        {
            _logOutService = logOutService;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _logOutService.LogOutUserAsync();    
            
            return RedirectToPage("/Index");
        }
    }
}
