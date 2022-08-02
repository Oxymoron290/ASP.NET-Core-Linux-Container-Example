using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohoWineryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Coho-Winery app started...");
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Checking for token...");
            var host = CreateHostBuilder(args).Build();
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogInformation("Creating Database...");
                    var db = scope.ServiceProvider.GetRequiredService<Data.VineyardContext>();
                    db.Database.Migrate();
                    logger.LogInformation("Database Created...");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
