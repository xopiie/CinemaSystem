using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;
using Uzunova_Nadica_1002387434_DSR_2021.Data;

[assembly: HostingStartup(typeof(Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.IdentityHostingStartup))]
namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Configures the web host builder to set up the identity services.
        /// </summary>
        /// <param name="builder">The web host builder used to configure services.</param>
        /// <remarks>
        /// This method sets up the dependency injection for the identity services in the application.
        /// It adds the Entity Framework Core DbContext for managing user identities using SQL Server.
        /// The connection string for the database is retrieved from the application configuration.
        /// Additionally, it configures the default identity options, allowing users to sign in without requiring a confirmed account.
        /// The method also adds role management capabilities and integrates the identity services with the Entity Framework stores.
        /// </remarks>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<Uporabnik>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                })
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}