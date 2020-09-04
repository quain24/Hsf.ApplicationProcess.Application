using FluentValidation;
using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Hsf.ApplicationProcess.August2020.Domain.Validators;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using MatBlazor;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<ApplicantApiService>();
            builder.Services.AddHttpClient<CountryValidator>("RestCountries", client => client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/"));

            // -- Left for publishing - Error - blazor wasm cannot resolve HttpFactory - issue made on github
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });
            //builder.Services.AddScoped<ApplicantApiService>();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddTransient<EmailValidator>();

            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 3000;
            });

            await builder.Build().RunAsync();
        }
    }
}