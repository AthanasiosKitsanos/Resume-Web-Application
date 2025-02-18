using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeProject.Services;
using ResumeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeProject.Pages.Account;
public class MyProfileModel : PageModel
{
    private readonly UserInfoService _userInfoService;

    public MyProfileModel(UserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    public List<UserInfoDTO>? UserInfo { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Fetch the logged-in user's information asychronously
        UserInfo = await _userInfoService.GetLoggedInUserInfoAsync();

        if (UserInfo is null || UserInfo.Count == 0)
        {
            return NotFound();
        }

        return Page();
    }
}