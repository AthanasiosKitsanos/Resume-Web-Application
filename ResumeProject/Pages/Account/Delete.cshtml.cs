using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;

namespace ResumeProject.Pages.Account;

public class DeleteModel : PageModel
{
    private readonly DeleteService _deleteService;

    public DeleteModel(DeleteService deleteService)
    {
        _deleteService = deleteService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _deleteService.DeleteUserAsync();

        if(!result)
        {
            return Page();
        }

        return RedirectToPage("/Index");
    }
}

