using Course.Web.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Services.Abstract
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCoursesAsync();
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<List<CourseViewModel>> GetAllCoursesByUserIdAsync(string userId);

        Task<CourseViewModel> GetByCourseId(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateModel courseCreateInput);

        Task<bool> UpdateCourseAsync(CourseUpdateModel courseUpdateInput);

        Task<bool> DeleteCourseAsync(string courseId);
    }
}
