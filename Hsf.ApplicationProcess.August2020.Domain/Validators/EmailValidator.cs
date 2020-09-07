using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Hsf.ApplicationProcess.August2020.Domain.Validators
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