using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Hsf.ApplicationProcess.August2020.Domain.Models;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(a => a.ID).GreaterThanOrEqualTo(0);
            RuleFor(a => a.Name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(a => a.Age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.Hired).NotNull();
            RuleFor(a => a.CountryOfOrigin).NotNull().SetValidator(new CountryValidator());
            RuleFor(a => a.EmailAddress).EmailAddress();
        }
    }
}
