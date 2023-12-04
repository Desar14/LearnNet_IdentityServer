using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using LearnNet_IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LearnNet_IdentityServer
{
    public class CustomProfileService : ProfileService<ApplicationUser>
    {
        public CustomProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
        {
            var principal = await GetUserClaimsAsync(user);
            var id = (ClaimsIdentity)principal.Identity;
            
            id.AddClaim(new Claim(ClaimTypes.Role, ""));
            foreach (var item in context.RequestedResources.Resources.ApiResources)
            {
                id.AddClaim(new Claim(ClaimTypes.Role, item.Name));
            }

            context.AddRequestedClaims(principal.Claims);
        }
    }
}
