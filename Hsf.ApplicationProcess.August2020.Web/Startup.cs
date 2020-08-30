using FluentValidation;
using FluentValidation.AspNetCore;
using Hsf.ApplicationProcess.August2020.Data;
using Hsf.ApplicationProcess.August2020.Data.Repositories;
using Hsf.ApplicationProcess.August2020.Web.SwaggerExamples;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Hsf.ApplicationProcess.August2020.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Turned off automatic 400 code response on error to enable error logging in controllers
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // Inject an implementation of ISwaggerProvider
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HSF Applicant Demo API", Version = "v0.1" });
                c.ExampleFilters();
                c.EnableAnnotations();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "HSFApi.xml"));

                c.AddFluentValidationRules();

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<ApplicantExample>();

            services.AddDbContext<ApplicantsDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Applicants"));

            // Working repository
            services.AddScoped<IApplicantRepository, InMemoryRepository>();

            // Disable translations in fluent validation
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HSF ApplicationProcess");
                c.DocumentTitle = "HSF application demo";
            });
        }
    }
}