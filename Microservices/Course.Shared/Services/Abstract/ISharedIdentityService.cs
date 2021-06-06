using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.Services.Abstract
{
    public interface ISharedIdentityService
    {
        //Get User Token
        public string GetUserId { get;}
    }
}
