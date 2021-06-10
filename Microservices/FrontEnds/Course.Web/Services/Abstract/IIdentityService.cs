using Course.Shared.Dtos;
using Course.Web.Models;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Services.Abstract
{
    //This service has a Signin GetAccessTokenByRefresh and RevokeRefreshToken. We will get token from identityserver and we will save in Cookies
    //then if user is not logged out we will Get access token by refresh token.
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);


        //we will use TokenResponse class in IdentityModel library -- Look nuget packages for this (IdentityModel)
        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
