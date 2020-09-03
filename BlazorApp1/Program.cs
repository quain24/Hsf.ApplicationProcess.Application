using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorApp1.ApiServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/applicants") });

            builder.Services.AddScoped<ApplicantApiService>();

            //builder.Services.AddHttpClient<ApplicantApiService>(client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5011/api/applicants");
            //});

            await builder.Build().RunAsync();
            await builder.Build().RunAsync();
        }
    }
}
