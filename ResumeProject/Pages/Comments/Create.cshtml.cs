using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Models;
using ResumeProject.Services;

namespace ResumeProject.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly CreateCommentsService _createCommentsService;

        public CreateModel(CreateCommentsService createCommentsService)
        {
            _createCommentsService = createCommentsService;
        }
        

        [BindProperty]
        public CommentDTO Input { get; set; } = new CommentDTO();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _createCommentsService.CreateCommentsServicesAsync(Input);

            if(result)
            {
                return RedirectToPage("/Comments/GetAllComments");
            }

            return Page();
        }
    }
}
