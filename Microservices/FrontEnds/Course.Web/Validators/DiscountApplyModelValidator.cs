using Course.Web.Models.Discounts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Validators
{
    public class DiscountApplyModelValidator:AbstractValidator<DiscountApplyModel>
    {
        public DiscountApplyModelValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Coupon code field is required");
        }
    }
}
