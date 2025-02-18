using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace ResumeProject.Pages.Account;


[Authorize(Roles = "Admin")]
public class GetAllUsersModel : PageModel
{
    private readonly UserInfoService _userInfoService;

    public GetAllUsersModel(UserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    public List<UserInfoDTO>? AllUsersList { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Fetch the logged-in user's information asychronously
        AllUsersList = await _userInfoService.GetAllUserInfo();

        if (AllUsersList is null || AllUsersList.Count == 0)
        {
            return NotFound();
        }

        return Page();
    }
}