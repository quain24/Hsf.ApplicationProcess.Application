using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });

            builder.Services.AddScoped<ApplicantApiService>(sp => new ApplicantApiService(sp.GetRequiredService<HttpClient>()));
            //builder.Services.AddScoped(sp => new ApplicantApiService(new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") }));
            
            await builder.Build().RunAsync();
        }
    }
}
