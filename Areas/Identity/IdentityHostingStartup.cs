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