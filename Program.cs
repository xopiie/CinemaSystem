using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021
{
    public class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the application.</param>
        /// <remarks>
        /// This method is the starting point of the application. It creates a host builder using the <see cref="CreateHostBuilder"/> method,
        /// builds the host, and then runs it. The host is responsible for managing the application's lifecycle and services.
        /// The command-line arguments can be used to configure the application at startup, allowing for dynamic behavior based on user input.
        /// </remarks>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a host builder for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments.</param>
        /// <returns>An instance of <see cref="IHostBuilder"/> configured for the application.</returns>
        /// <remarks>
        /// This method initializes a new instance of the host builder using the default settings provided by the Host class.
        /// It configures the web host with default settings and specifies the startup class to be used for the application.
        /// The startup class is typically where services are configured and the request handling pipeline is defined.
        /// This setup is essential for running a web application in ASP.NET Core, allowing for dependency injection and middleware configuration.
        /// </remarks>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
