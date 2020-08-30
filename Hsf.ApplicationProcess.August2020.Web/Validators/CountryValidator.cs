using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using Hsf.ApplicationProcess.August2020.Web.Extensions;
using Microsoft.AspNetCore.Http;

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
            string url = $"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url, cancellation);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
