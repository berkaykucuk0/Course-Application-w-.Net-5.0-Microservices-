using Course.Shared.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Course.Shared.Services.Concrede
{
    public class SharedIdentityService : ISharedIdentityService
    {
        //Get User Token
        IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.Claims.Where(x=>x.Type=="sub").FirstOrDefault().Value;
    }
}
