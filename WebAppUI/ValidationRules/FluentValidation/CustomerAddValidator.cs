using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.ValidationRules.FluentValidation
{
    public class CustomerAddValidator:AbstractValidator<Customer>
    {
        public CustomerAddValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name required");
            RuleFor(s => s.Name).MinimumLength(2);
            RuleFor(s => s.Surname).NotEmpty().WithMessage("Surname required");
            RuleFor(s => s.Surname).MinimumLength(2);
            RuleFor(s => s.PhoneNumber).NotEmpty().WithMessage("Phone number required");
            RuleFor(s => s.PhoneNumber).Length(10);
            RuleFor(s => s.Password).NotEmpty().WithMessage("Password required");
        }
    }
}
