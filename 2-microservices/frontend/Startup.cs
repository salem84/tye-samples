using AspNetCore.VersionInfo;
using AspNetCore.VersionInfo.Providers;
using frontend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddTransient<WeatherForecastService>();
            services.AddTransient<PersonService>();
            services.AddTransient<TodoService>();

            services.AddHttpClient<api.personApi.IPersonApiClient, api.personApi.PersonApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5007");
            });
            services.AddHttpClient<api.todoApi.ITodoApiClient, api.todoApi.TodoApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005");
            });
            services.AddHttpClient<api.weatherApi.IWeatherApiClient, api.weatherApi.WeatherApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5003");
            });

            services.AddVersionInfo()
                .With<ClrVersionProvider>()
                .With<EnvironmentVariablesProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapVersionInfo();
            });
        }
    }
}
