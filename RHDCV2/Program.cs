using DAL.DbRHDCV2Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Configuration;

namespace AutoRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    // Add appsettings.json configuration
                    configBuilder.SetBasePath("C:\\Users\\PC\\Documents\\GitHub\\RHDCV2\\RHDCV2")
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None); // Disable SQL logging
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.None);    // Disable infrastructure logging
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Retrieve the configuration from hostContext.Configuration
                    var configuration = hostContext.Configuration;

                    // Use the connection string from the configuration
                    var connectionString = configuration.GetConnectionString("SQLServer");

                    services.Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromMinutes(2));
                    services.AddHostedService<AutoRetrieverWorker>();

                    services.AddDbContextPool<RHDCV2Context>(options =>
                        options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(120))
                    );

                    // Register other services
                    ServicesConfig.Register(services);
                });
        }
    }
}