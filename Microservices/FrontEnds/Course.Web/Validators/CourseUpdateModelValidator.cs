using Course.Web.Models.Courses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Validators
{
    public class CourseUpdateModelValidator:AbstractValidator<CourseUpdateModel>
    {
        public CourseUpdateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description field is required");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Time field is required");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field is required").ScalePrecision(2, 6).WithMessage("Wrong format");
        }
    }
}
