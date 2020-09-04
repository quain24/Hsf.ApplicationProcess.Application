using FluentValidation;
using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Hsf.ApplicationProcess.August2020.Blazor.ViewModels;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Hsf.ApplicationProcess.August2020.Domain.Validators;
using Microsoft.Extensions.Localization;

namespace Hsf.ApplicationProcess.August2020.Blazor.Validators
{
    public class ApplicantInsertViewModelValidator : AbstractValidator<ApplicantInsertViewModel>
    {
        private readonly CountryValidator _countryValidator;

        public ApplicantInsertViewModelValidator(CountryValidator countryValidator)
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
                .WithMessage(applicant => "validation.err_no_such_country");
            RuleFor(a => a.emailAddress).NotNull()
                .EmailAddress()
                .SetValidator(new EmailValidator())
                .WithMessage("validation.err_bad_email_format");
        }
    }
}