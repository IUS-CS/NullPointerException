using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

using Chronos.Models;

namespace Chronos
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity
    // and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        private async Task ReplaceClaims(string userId, params Claim[] newClaims)
        {
            var oldClaims = await UserManager.GetClaimsAsync(userId);

            foreach (var newClaim in newClaims.Where(nc => nc != null))
            {
                foreach (var oldClaim in oldClaims.Where(oc => oc.Type == newClaim.Type))
                {
                    await UserManager.RemoveClaimAsync(userId, oldClaim);
                }

                await UserManager.AddClaimAsync(userId, newClaim);
            }
        }

        public async override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var externalIdentity = await AuthenticationManager.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ExternalCookie);
            if (externalIdentity != null)
            {
                // ***
                // Copy the claim that our external authentication provider set (in Startup.Auth.cs) over
                // to the user's application identity.

                var googleUserId = externalIdentity.FindFirst(MyClaimTypes.GoogleUserId);

                await ReplaceClaims(user.Id, googleUserId);
            }

            var identity = await user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            return identity;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<UserManager<ApplicationUser>>(),
                context.Authentication);
        }
    }
}