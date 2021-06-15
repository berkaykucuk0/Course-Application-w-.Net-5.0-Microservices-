using Course.Shared.Dtos;
using Course.Web.ClientsInfo;
using Course.Web.Helpers;
using Course.Web.Models.Courses;
using Course.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;
        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            //http:localhost:5000/services/catalog/category
            var response = await _httpClient.GetAsync("category");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseData.Data;
        }
        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            //http:localhost:5000/services/catalog/course
            var response = await _httpClient.GetAsync("course");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            //Get photo from photostock
            responseData.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseData.Data;
        }
        public async Task<List<CourseViewModel>> GetAllCoursesByUserIdAsync(string userId)
        {
            //http:localhost:5000/services/catalog/course/GetAllByUserId/{userId}
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            //Get photo from photostock
            responseData.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseData.Data;
        }
        public async Task<bool> CreateCourseAsync(CourseCreateModel courseCreateInput)
        {
            var resultPhoto = await _photoStockService.UploadImage(courseCreateInput.PhotoFormFile);
            if (resultPhoto!=null)
            {
                courseCreateInput.Picture = resultPhoto.Url;
            }
           
            //PostAsJsonAsync: This method automatically converts our data to json and makes request to api. more useful than PostAsync() method.
            var response = await _httpClient.PostAsJsonAsync<CourseCreateModel>("course", courseCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"course/{courseId}");
            return response.IsSuccessStatusCode;
        }    
        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            //http:localhost:5000/services/catalog/courseId
            var response = await _httpClient.GetAsync($"course/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return responseData.Data;
        }
        public async Task<bool> UpdateCourseAsync(CourseUpdateModel courseUpdateInput)
        {
            //Update photo 
            var resultPhotoService = await _photoStockService.UploadImage(courseUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                //if new photo is uploaded delete old photo
                await _photoStockService.DeleteImage(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }

            //PostAsJsonAsync: This method automatically converts our data to json and makes request to api. more useful than PostAsync() method.
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateModel>("course", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
