using Hsf.ApplicationProcess.August2020.Blazor.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.Extensions
{
    public static class ApplicantInsertModelExtensions
    {
        public static bool IsDefault(this ApplicantInsertModel model)
        {
            return string.IsNullOrEmpty(model.address) &&
                string.IsNullOrEmpty(model.countryOfOrigin) &&
                string.IsNullOrEmpty(model.emailAddress) &&
                string.IsNullOrEmpty(model.familyName) &&
                string.IsNullOrEmpty(model.name) &&
                model.age == 0;
        }
    }
}