using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Business.Models.Validations
{
    class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 200).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 1000).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
