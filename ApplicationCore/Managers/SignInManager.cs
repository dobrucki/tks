using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VMRent.Models;

namespace VMRent.Managers
{
    public class SignInManager : SignInManager<User>
    {
        public SignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            var user = UserManager.FindByNameAsync(userName);
            if (user.Result == null)
            {
                throw new InvalidOperationException("User does not exist");
            }
            
            return !user.Result.Active ? 
                Task.FromResult(SignInResult.LockedOut) : 
                base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
        
        
    }
}