using FluentValidation;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private readonly IStringLocalizer _localizer;

        public ApplicantValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(a => a.ID).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(a => a.Name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.Age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.Hired).NotNull();
            RuleFor(a => a.CountryOfOrigin)
                .NotNull()
                .SetValidator(new CountryValidator())
                .WithMessage(applicant => _localizer["validation.err_no_such_country", new { countryName = applicant.CountryOfOrigin }].Value);
            RuleFor(a => a.EmailAddress).NotNull()
                .EmailAddress()
                .SetValidator(new EmailValidator())
                .WithMessage(_localizer["validation.err_bad_email_format"].Value);
        }
    }
}