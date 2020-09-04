using FluentValidation.Validators;
using Hsf.ApplicationProcess.August2020.Domain.Extensions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Domain.Validators
{
    public class CountryValidator : AsyncValidatorBase
    {
        private readonly HttpClient _httpClient;
        private bool _lastCheckResult;
        private string _lastChecked = string.Empty;

        public CountryValidator(HttpClient httpClient) : base(string.Empty)
        {
            _httpClient = httpClient;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            if (context.PropertyValue is null)
                return false;

            if (_lastChecked == context.PropertyValue.ToString())
                return _lastCheckResult;

            _lastChecked = context.PropertyValue.ToString();

            // Shortest country name is 4, longest - 56 - no need for api abuse
            if (CountryNameLengthCheck(context.PropertyValue.ToString()))
                return _lastCheckResult = false;

            var countryName = context.PropertyValue.ToString().Urlify();
            var url = $"name/{countryName}?fullText=true";
            var resp = await _httpClient.GetAsync(url, cancellation);

            return _lastCheckResult = resp.IsSuccessStatusCode;
        }

        private bool CountryNameLengthCheck(string name) =>
            name.Length < 4 || name.Length > 56;
    }
}