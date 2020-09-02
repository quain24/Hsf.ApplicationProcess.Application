using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using Hsf.ApplicationProcess.August2020.Domain.Extensions;

namespace Hsf.ApplicationProcess.August2020.Domain.Validators
{
    public class CountryValidator : AsyncValidatorBase
    {
        private readonly HttpClient _httpClient;

        public CountryValidator(HttpClient httpClient) : base(string.Empty)
        {
            _httpClient = httpClient;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            if (context.PropertyValue is null)
                return false;

            var countryName = context.PropertyValue.ToString().Urlify();
            var url = $"name/{countryName}?fullText=true";
            var resp = await _httpClient.GetAsync(url, cancellation);

            return resp.IsSuccessStatusCode;
        }
    }
}