using Forex.Entities;
using Forex.Entities.Repository;
using Forex.Entities.UOW;
using Forex.Services.FixerService;
using Forex.Services.ForexService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Forex.Batch
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                Console.Title = "Daily Exchange Rate Processing - Batch";

                var container = RegisterServices();
                var _forexService = container.GetRequiredService<IForexService>();

                Console.WriteLine("Starting batch");
                var data = _forexService.SyncLatestExchangeRatesInDB();
                Console.WriteLine("Processing ended");

                if (data)
                {
                    Console.WriteLine("Sync successed");
                }
                else
                {
                    Console.WriteLine("Nothing Synced");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error: " + ex.Message);
                Console.ReadKey();
            }

        }

        static IServiceProvider RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();
            // TODO
            // Regster Services
            services.AddSingleton<IConfiguration>(config);
            services.AddTransient<ForexDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IForexService, ForexService>();
            services.AddScoped<IFixerService, FixerService>();
            return services.BuildServiceProvider();
        }

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}
