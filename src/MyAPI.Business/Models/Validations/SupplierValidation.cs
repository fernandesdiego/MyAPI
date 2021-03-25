using FluentValidation;
using MyAPI.Business.Models.Validations.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .Length(2, 100).WithMessage("The field {PropertyName} must have between {MinLenght} and {MaxLength} characters.");
            When(x => x.SupplierType == SupplierType.PF, () =>
            {
                RuleFor(x => x.Document.Length).Equal(CpfValidation.CpfSize).WithMessage("The document field must have {ComparisonValue} characters.");
                RuleFor(x => CpfValidation.Validate(x.Document)).Equal(true).WithMessage("The document provided is invalid.");
            });

            When(f => f.SupplierType == SupplierType.PJ, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.CnpjSize).WithMessage("The Document field must have {ComparisonValue} characters.");
                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true).WithMessage("The document provided is invalid.");
            });
        }
    }
}
