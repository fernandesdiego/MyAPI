using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");
            RuleFor(x => x.District)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");
            RuleFor(x => x.ZIP)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(8).WithMessage("The field {PropertyName} must have {MaxLenght} characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(2, 50).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(1, 50).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");






        }
    }
}
