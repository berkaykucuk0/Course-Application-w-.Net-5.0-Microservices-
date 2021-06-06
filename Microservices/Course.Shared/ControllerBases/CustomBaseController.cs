using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.ControllerBases
{
    public class CustomBaseController:ControllerBase
    {
        //We will use this function for all controllers ( for example:  return CreateActionResultInstance(response);)
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
