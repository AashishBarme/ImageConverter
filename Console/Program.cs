// See https://aka.ms/new-console-template for more information
namespace Console;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    static void Main(string[] args)
    {
         CreateHostBuilder(args).Build().Run();
        Console.WriteLine("Hello, World!");

    }

     public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = BuildConfig();
                    Console.WriteLine(configuration["FilePath"]);
                });

      static IConfigurationRoot BuildConfig()
        {
            
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            
            .AddEnvironmentVariables()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)

            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true);
            return builder.Build();

        }

}

