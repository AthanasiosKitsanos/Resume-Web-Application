using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;

namespace ResumeProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly AccountServices _accountServices;

        public LogoutModel(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _accountServices.IsLoggedIn();

            if(result)
            {
                await _accountServices.LogOutUser();    
            }
            
            return RedirectToPage("/");
        }
    }
}
