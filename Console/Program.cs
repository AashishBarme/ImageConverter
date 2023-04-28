// See https://aka.ms/new-console-template for more information
namespace Console;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Converter;

public class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        
    }

     public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = BuildConfig();
                    // services.AddSingleton<ImageFormatService>();
                    var x  =  new ImageConverter();
                    x.Convert(configuration);
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

