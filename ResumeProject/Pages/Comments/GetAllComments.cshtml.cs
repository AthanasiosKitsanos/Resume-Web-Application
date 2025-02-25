using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;

namespace ResumeProject.Pages.Comments
{
    public class GetAllCommentsModel : PageModel
    {
        private readonly GetCommentsService _getCommentsService;

        public GetAllCommentsModel(GetCommentsService getAllCommentsService)
        {
            _getCommentsService = getAllCommentsService;
        }

        public List<CommentDTO> AllComments { get; set; } = new List<CommentDTO>();

        public async Task<IActionResult> OnGetAsync()
        {
            AllComments = await _getCommentsService.GetAllCommentsAsync();

            if(AllComments is not null)
            {
                return Page();
            }

            return NotFound();
        }
    }
}
