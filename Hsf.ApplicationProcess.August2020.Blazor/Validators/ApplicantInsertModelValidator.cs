using FluentValidation;
using Hsf.ApplicationProcess.August2020.Blazor.Models;
using Hsf.ApplicationProcess.August2020.Domain.Validators;

namespace Hsf.ApplicationProcess.August2020.Blazor.Validators
{
    public class ApplicantInsertModelValidator : AbstractValidator<ApplicantInsertModel>
    {
        private readonly CountryValidator _countryValidator;

        public ApplicantInsertModelValidator(CountryValidator countryValidator)
        {

            _countryValidator = countryValidator;
            RuleFor(a => a.name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.familyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.hired).NotNull();
            RuleFor(a => a.countryOfOrigin)
                .NotNull()
                .SetValidator(_countryValidator)
                .WithErrorCode(Global.ValidatorConstants.CountryErrorCode);
            RuleFor(a => a.emailAddress).EmailAddress().WithMessage(" ").NotNull();
        }
    }
}