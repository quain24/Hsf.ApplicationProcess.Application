using FluentValidation;
using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Hsf.ApplicationProcess.August2020.Blazor.Validators;
using Hsf.ApplicationProcess.August2020.Domain.Validators;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<ApplicantApiService>();

            // -- Left for publishing - Error - blazor wasm cannot resolve HttpFactory sometimes when published - issue made on github
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });
            //builder.Services.AddScoped<ApplicantApiService>();

            builder.Services.AddI18nText();
            builder.Services.AddSingleton(cp =>
                new ApplicantInsertModelValidator(cp.GetService<CountryValidator>()));

            builder.Services.AddSingleton(cp => new ApplicantInsertModelValidator(cp.GetService<CountryValidator>()));

            //builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddHttpClient<CountryValidator>("RestCountries", client => client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/"));
            builder.Services.AddTransient<ToastGenerator>();

            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomFullWidth;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 3000;
                config.ShowProgressBar = true;
            });

            await builder.Build().RunAsync();
        }
    }
}