using Course.Shared.Dtos;
using Course.Web.Models.PhotoStocks;
using Course.Web.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteImage(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photo?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PictureStockViewModel> UploadImage(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            // example photo = 203802340234.jpg
            var randonFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randonFilename);

            var response = await _httpClient.PostAsync("photo", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                //Logging
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<PictureStockViewModel>>();

            return responseSuccess.Data;
        }
    }
}
