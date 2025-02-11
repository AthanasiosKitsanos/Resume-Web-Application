using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using ResumeProject.Models;

namespace ResumeProject.Services;

public class ApplicationUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor): base (userManager, roleManager, optionsAccessor)
    {

    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        // Start with the default claims (which usually are the user name/email)
        var identity = await base.GenerateClaimsAsync(user);

        // Add custom claims for FirstName and LastName using the Database values.
        identity.AddClaim(new Claim("FirstName", user.FirstName ?? string.Empty));
        identity.AddClaim(new Claim("LastName", user.LastName ?? string.Empty));

        return identity;
    }
}
