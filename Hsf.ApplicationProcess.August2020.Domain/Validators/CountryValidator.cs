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

            var countryName = context.PropertyValue.ToString();

            if (_lastChecked == countryName)
                return _lastCheckResult;
            _lastChecked = countryName;

            // Shortest country name is 4, longest - 56 - no need for api abuse
            if (CountryNameLengthCheck(countryName))
                return _lastCheckResult = false;

            var url = $"name/{countryName.Urlify()}?fullText=true";
            var resp = await _httpClient.GetAsync(url, cancellation);

            return _lastCheckResult = resp.IsSuccessStatusCode;
        }

        private bool CountryNameLengthCheck(string name) =>
            name.Length < Global.ValidatorConstants.ShortestNameLengthLength || name.Length > Global.ValidatorConstants.LongestCountryNameLength;
    }
}