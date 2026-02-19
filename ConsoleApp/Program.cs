using Application;
using Infrastructure.Contexties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ClosedXML.Excel;
using Application.Interface;


internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Inicio");

        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false)
                            .AddJsonFile($"appsettings.Development.json", optional: true)
                            .AddEnvironmentVariables();

        builder.Services.addApplicationService(builder.Configuration);


        var host = builder.Build();

        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        try
        {
            await ProcessSheetsAsync(serviceProvider);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }


        static async Task ProcessSheetsAsync(IServiceProvider serviceProvider)
        {
            //var ctx = serviceProvider.GetRequiredService<AppDbContext>();

            var _service = serviceProvider.GetRequiredService<ISheetService>();


            try
            {
                Console.WriteLine("Inicio - Sheets");
                await _service.GetSheetAsync();
                Console.WriteLine("Fim - Sheets");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        
    }
}