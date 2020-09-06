using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Hsf.ApplicationProcess.August2020.Blazor.Validators;
using Hsf.ApplicationProcess.August2020.Domain.Validators;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            await LoadAppSettingsAsync(builder);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<ApplicantApiService>();

            // -- Left for publishing - Error - blazor wasm cannot resolve HttpFactory sometimes when published - issue made on github
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });
            //builder.Services.AddScoped<ApplicantApiService>();

            builder.Services.AddI18nText();

            builder.Services.AddHttpClient<CountryValidator>("RestCountries",
                client => client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings")["restCountriesApiBaseAddress"]));
            builder.Services.AddSingleton(sp => new ApplicantInsertModelValidator(sp.GetService<CountryValidator>()));

            builder.Services.AddHttpClient<ApplicantApiService>("ApplicantApiServices",
                client => client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings")["hsfApiBaseAddress"]));

            builder.Services.AddScoped<ToastGenerator>();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomFullWidth;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 4000;
                config.ShowProgressBar = true;
            });

            await builder.Build().RunAsync();
        }

        private static async Task LoadAppSettingsAsync(WebAssemblyHostBuilder builder)
        {
            // read JSON file as a stream for configuration
            var client = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            // the appsettings file must be in 'wwwroot'
            using var response = await client.GetAsync("appsettings.json");
            using var stream = await response.Content.ReadAsStreamAsync();
            builder.Configuration.AddJsonStream(stream);
            client.Dispose();
        }
    }
}