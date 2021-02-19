using Forex.DomainModels.Integration;
using Forex.Entities;
using Forex.Services.FixerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Forex.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "FOREX - CURRENCY CONVERSION";
            try
            {
                var container = RegisterServices();
                var _fixerService = container.GetRequiredService<IFixerService>();

                bool exitProgram = false;

                do
                {
                    Console.Write("Want to get currency conversion with current(Y) or past-dated(N) rate?(Y/N): ");
                    var answar1 = Console.ReadLine().Trim();

                    DateTime? date = null;
                    if (answar1.ToUpper() == "N")
                    {
                        Console.Write("Please enter date in YYYY-MM-DD format: ");
                        var input = Console.ReadLine().Trim();
                        DateTime outDate;
                        if (DateTime.TryParse(input, out outDate))
                        {
                            date = outDate;
                        }
                        else
                        {
                            throw new Exception("Invalid Input");
                        }
                    }

                    Console.Write("Please enter first currency code: ");
                    var firstCurrencyCode = Console.ReadLine().Trim();                    

                    Console.Write("Please enter curency amount to exchange: ");
                    var inputAmount = Console.ReadLine().Trim();
                    decimal outAmount;
                    if (!Decimal.TryParse(inputAmount, out outAmount))
                    {
                        throw new Exception("");
                    }

                    Console.Write("Please enter second currency code: ");
                    var secondCurrencyCode = Console.ReadLine().Trim();
                    
                    var model = new CurrencyConversionDomainModel()
                    {
                        FirstCurrencyCode = firstCurrencyCode.ToUpper(),
                        CurrencyAmount = outAmount,
                        SecondCurrencyCode = secondCurrencyCode.ToUpper(),
                        ExchangeDate = date
                    }; 
                    var result = _fixerService.CurrencyConversion(model);

                    if (result.ExchangeDate.HasValue)
                    {
                        Console.WriteLine($"{result.ExchangeDate}");
                    }                    
                    Console.WriteLine($"{result.CurrencyAmount} {result.FirstCurrencyCode} = {result.ExchangedAmount} {result.SecondCurrencyCode}");

                    Console.Write("Want to close application?(Y/N): ");
                    var answar2 = Console.ReadLine().Trim();

                    if (answar2.ToUpper() == "Y")
                    {
                        exitProgram = true;
                    }

                } while (exitProgram);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static IServiceProvider RegisterServices()
        {            
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();
            //TODO
            //Register Services
            services.AddSingleton<IConfiguration>(config);
            services.AddTransient<IFixerService, FixerService>();
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
