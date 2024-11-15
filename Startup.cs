using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection to which services are added.</param>
        /// <remarks>
        /// This method is called by the runtime to add services to the dependency injection container.
        /// It sets up the necessary services for the application, including MVC controllers with views,
        /// Razor Pages, and authentication services. Specifically, it configures Facebook authentication
        /// by providing the App ID and App Secret, as well as specifying a path to redirect users if 
        /// access is denied. This setup is essential for enabling user authentication and authorization 
        /// in the application.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "915757005823081";
                facebookOptions.AppSecret = "322d5c40f8879703004deaf8bc774006";
                facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline for the application.
        /// </summary>
        /// <param name="app">The application builder used to configure the HTTP request pipeline.</param>
        /// <param name="env">The web hosting environment information.</param>
        /// <remarks>
        /// This method is called by the runtime to set up the middleware components that handle requests and responses.
        /// It checks if the application is running in a development environment and configures the developer exception page accordingly.
        /// In production, it sets up a generic exception handler and enables HTTP Strict Transport Security (HSTS).
        /// The method also configures HTTPS redirection, static file serving, routing, authentication, and authorization.
        /// Finally, it defines the endpoint routing for MVC controllers and Razor pages, setting a default route pattern.
        /// </remarks>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
