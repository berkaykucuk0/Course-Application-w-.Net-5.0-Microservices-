using Course.Shared.Services.Abstract;
using Course.Web.Models.Courses;
using Course.Web.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Controllers
{
    //[Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCoursesByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateModel courseCreateModel)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }

            //user id with jwt
            courseCreateModel.UserId = _sharedIdentityService.GetUserId;
            await _catalogService.CreateCourseAsync(courseCreateModel);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategoriesAsync();

            if (course == null)
            {
                
                RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);
            CourseUpdateModel courseUpdateModel = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(courseUpdateModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateModel courseUpdateModel)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateModel.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _catalogService.UpdateCourseAsync(courseUpdateModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
