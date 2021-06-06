using Course.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services
{

    //Resource Owner credentials (clientId/clientsecret/email/password)
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

      

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userCheck = await _userManager.FindByEmailAsync(context.UserName);
            if (userCheck == null)
            {
                //if you want you don't write here
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });
                context.Result.CustomResponse = errors;


                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(userCheck, context.Password);

            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });
                context.Result.CustomResponse = errors;

                return;
            }
            context.Result = new GrantValidationResult(userCheck.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
