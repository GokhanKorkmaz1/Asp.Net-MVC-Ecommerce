using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.ValidationRules.FluentValidation
{
    public class ProductAddValidator:AbstractValidator<Product>
    {
        public ProductAddValidator()
        {
            RuleFor(p => p.CategoryId).InclusiveBetween(1, 8);
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Category Id required");
            RuleFor(p => p.ImagePath).NotEmpty().WithMessage("Image path required");
            RuleFor(p => p.Producer).NotEmpty().WithMessage("Producer name required");
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model name required");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Price required");
            RuleFor(p => p.UnitinStock).NotEmpty().WithMessage("Stock value required");
        }
    }
}
