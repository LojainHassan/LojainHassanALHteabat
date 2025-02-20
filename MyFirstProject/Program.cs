using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstProject.Models;
using MyFirstProject.Models.Entities;
using System;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;
using MyFirstProject.Services;
using MyFirstProject.Contracts;

class Program
{
    static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        // Apply migrations at startup
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>(); 
            ///Second Objective: (CRUD Operations Using Entity Framework Core)
            ///After create database apply CRUD operation in EF core on employee entity as below:  
            ///1.Add about new 20 departments by use DB context and one save changes to
            ///database.
            DepartmentServices departmentService = new DepartmentServices(context);
           await departmentService.Add20Departments();

        }
        Console.WriteLine("Application has started.");
        Console.ReadKey();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer("Server=.;Database=EmployeeManagement;Trusted_Connection=True;TrustServerCertificate=True;"));
            });
}
