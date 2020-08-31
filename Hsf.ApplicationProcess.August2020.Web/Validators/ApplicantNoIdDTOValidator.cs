using FluentValidation;
using Hsf.ApplicationProcess.August2020.Web.DTO;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class ApplicantNoIdDTOValidator : AbstractValidator<ApplicantNoIdDTO>
    {
        public ApplicantNoIdDTOValidator()
        {
            RuleFor(a => a.name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.familyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.hired).NotNull();
            RuleFor(a => a.countryOfOrigin).NotNull().SetValidator(new CountryValidator());
            RuleFor(a => a.emailAddress).NotNull().EmailAddress();
        }
    }
}