using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class EmailValidator : PropertyValidator
    {
        public EmailValidator() : base(string.Empty)
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue is string email && Regex.IsMatch(email, "^[a-zA-Z0-9.]{1,}@[a-zA-Z0-9]{1,}\\.[a-zA-Z]{2,3}$");
        }
    }
}
