using BlazorMovies.SharedBackend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public class AuthenticationStateServiceWebAssembly : IAuthenticationStateService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public AuthenticationStateServiceWebAssembly(IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<string> GetCurrentUserId()
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var user = await userManager.FindByEmailAsync(httpContextAccessor.HttpContext.User.Identity.Name);
            return user.Id;
        }
    }
}
