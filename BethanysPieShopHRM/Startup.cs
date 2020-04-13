using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysPieShopHRM
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
            services.AddHttpClient<Services.IEmployeeDataService, EmployeeDataService>(
                    client =>
                    {
                        //Dirección del cliente al que se conecta
                        client.BaseAddress = new Uri("https://localhost:44340/");
                    }
                );

            //Acceder al contexto de un http, el cual contiene información Ej. Usuario
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //  .AddCookie();
            services.AddAuthentication("Identity.Application")
               .AddCookie();
            services.AddHttpClient<ICountryDataService, CountryDataService>(
                   client =>
                   {
                        //Dirección del cliente al que se conecta
                        client.BaseAddress = new Uri("https://localhost:44340/");
                   }
               );
            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(
                   client =>
                   {
                       //Dirección del cliente al que se conecta
                       client.BaseAddress = new Uri("https://localhost:44340/");
                   }
               );
            services.AddServerSideBlazor()
                //Para obtener una vista detallada de errores
                     .AddCircuitOptions(
                        options => {
                            options.DetailedErrors = true;
                        });
            //services.AddSingleton<WeatherForecastService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
