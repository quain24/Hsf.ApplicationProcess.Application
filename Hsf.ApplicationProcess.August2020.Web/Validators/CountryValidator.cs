using FluentValidation.Validators;
using Hsf.ApplicationProcess.August2020.Web.Extensions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Web.Validators
{
    public class CountryValidator : AsyncValidatorBase
    {
        public CountryValidator() : base("Country name '{PropertyValue}' is not valid.")
        {
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var countryName = context.PropertyValue.ToString().Urlify();
            var url = $"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true";

            using var client = new HttpClient();
            var response = await client.GetAsync(url, cancellation);

            return response.IsSuccessStatusCode;
        }
    }
}