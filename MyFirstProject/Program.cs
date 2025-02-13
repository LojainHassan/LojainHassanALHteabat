using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstProject.Models;
using System;

class Program
{
    static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        // Apply migrations at startup
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();           
        }

        Console.WriteLine("Application has started.");
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer("Server=.;Database=EmployeeManagement;Trusted_Connection=True;TrustServerCertificate=True;"));
            });
}
