using FluentValidation;
using FluentValidation.Validators;
using Hsf.ApplicationProcess.August2020.Blazor.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.Validators
{
    public class ApplicantInsertModelValidator : AbstractValidator<ApplicantInsertModel>
    {
        public ApplicantInsertModelValidator(IPropertyValidator countryValidator)
        {
            RuleFor(a => a.name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.familyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.age).NotNull().NotEmpty().InclusiveBetween(20, 60);
            RuleFor(a => a.hired).NotNull();
            RuleFor(a => a.countryOfOrigin)
                .NotNull()
                .SetValidator(countryValidator)
                .WithErrorCode(Global.ValidatorConstants.CountryErrorCode);
            RuleFor(a => a.emailAddress).NotNull();
        }
    }
}