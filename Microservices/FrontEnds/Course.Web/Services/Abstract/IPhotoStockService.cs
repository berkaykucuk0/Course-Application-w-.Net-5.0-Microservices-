using Course.Web.Models.PhotoStocks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Services.Abstract
{
    public interface IPhotoStockService
    {
        Task<PictureStockViewModel> UploadImage(IFormFile photo);
        Task<bool> DeleteImage(string photoUrl);

    }
}
