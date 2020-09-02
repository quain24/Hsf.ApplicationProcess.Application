using FluentValidation;
using Hsf.ApplicationProcess.August2020.Domain.Validators;
using Hsf.ApplicationProcess.August2020.Web.DTO;
using Microsoft.Extensions.Localization;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class ApplicantNoIdDTOValidator : AbstractValidator<ApplicantNoIdDTO>
    {
        private readonly IStringLocalizer _localizer;
        private readonly CountryValidator _countryValidator;

        public ApplicantNoIdDTOValidator(IStringLocalizer localizer, CountryValidator countryValidator)
        {
            _localizer = localizer;
            _countryValidator = countryValidator;
            RuleFor(a => a.name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.familyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.hired).NotNull();
            RuleFor(a => a.countryOfOrigin)
                .NotNull()
                .SetValidator(_countryValidator)
                .WithMessage(applicant => _localizer["validation.err_no_such_country", new { countryName = applicant.countryOfOrigin }].Value);
            RuleFor(a => a.emailAddress).NotNull()
                .EmailAddress()
                .SetValidator(new EmailValidator())
                .WithMessage(_localizer["validation.err_bad_email_format"].Value);
        }
    }
}