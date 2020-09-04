using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<ApplicantApiService>();

            // -- Left for publishing - Error - blazor wasm cannot resolve HttpFactory - issue made on github
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });
            //builder.Services.AddScoped<ApplicantApiService>();

            await builder.Build().RunAsync();
        }
    }
}