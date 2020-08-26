using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using CsetAnalytics.Business;
using CsetAnalytics.Business.Analytics;
using CsetAnalytics.Business.Dashboard;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Factories;
using CsetAnalytics.Factories.Analytics;
using CsetAnalytics.Interfaces;
using CsetAnalytics.Interfaces.Analytics;
using CsetAnalytics.Interfaces.Dashboard;
using CsetAnalytics.Interfaces.Factories;
using CsetAnalytics.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;


namespace CsetAnalytics.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; set; }
        private IConfigurationRoot _config;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
            _config = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var Region = Configuration["AWSCognito:Region"];
            var PoolId = Configuration["AWSCognito:PoolId"];
            var AppClientId = Configuration["AWSCognito:AppClientId"];

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Audience = $"{AppClientId}";
                    options.Authority = $"https://cognito-idp.{Region}.amazonaws.com/{PoolId}";
                });
            
            services.AddControllers();
            services.AddSingleton(_config);

            services.AddAutoMapper(typeof(FactoryProfile));

            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<MongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            //Business
            services.AddTransient<IUserBusiness, UsersBusiness>();
            services.AddTransient<IAnalyticBusiness, AnalyticsBusiness>();
            services.AddTransient<IDashboardBusiness, DashboardBusiness>();

            //Factories
            services.AddTransient<IBaseFactory<AnalyticQuestionViewModel, AnalyticQuestionAnswer>, AnalyticQuestionModelFactory>();
            services.AddTransient<IBaseFactory<AnalyticQuestionAnswer, AnalyticQuestionViewModel>, AnalyticQuestionViewModelFactory>();
            services.AddTransient<IBaseFactory<AnalyticAssessmentViewModel, Assessment>, AnalyticAssessmentFactory>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MongoDbSettings settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CsetContext>();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });

            app.UseAuthentication();
            IMongoDbSettings dbSettings = settings;
            DatabaseInitializer.SeedCollections(dbSettings).GetAwaiter();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
