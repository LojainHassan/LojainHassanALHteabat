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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Principal;
using System.Threading.Tasks;

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
            ///await departmentService.Add20Departments();
            
            PositionService positionService = new PositionService(context);
            /// await positionService.Add20Positions();

            // Use constructor in Employee entity class to add (10) employees (by default
           /// vacation days left = 24 days) then added to database
            var employee = new Employee(context);
            // await employee.AddDefaultEmployees();

            ///4.Create method to update Employee main info and use employee number as
            /// unique key to find employee then update. (such as department, position, name,
            //salary)

            EmployeeService employeeService = new EmployeeService(context);

            // Ask for Employee Number to identify the employee to update
        Console.Write("Enter Employee Number to update: ");
            string employeeNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(employeeNumber))
            {
                Console.WriteLine("Employee Number is required.");
                return;
            }

            // Collect new employee data from the user
            //Console.Write("Enter Employee Name: ");
            //string employeeName = Console.ReadLine();

            //Console.Write("Enter Department ID: ");
            //int departmentId = int.Parse(Console.ReadLine());

            //Console.Write("Enter Position ID: ");
            //int positionId = int.Parse(Console.ReadLine());

            //Console.Write("Enter Gender Code (M/F): ");
            //string genderCode = Console.ReadLine();

            //Console.Write("Enter Reported To Employee Number (optional): ");
            //string reportedToEmployeeNumber = Console.ReadLine();

            //Console.Write("Enter Vacation Days Left: ");
            //int vacationDaysLeft = int.Parse(Console.ReadLine());

            //Console.Write("Enter Salary: ");
            //decimal salary = decimal.Parse(Console.ReadLine());

            //// Create a new Employee object with the updated values
            //// Create a new Employee object with the updated values using the constructor
            //var updatedEmployee = new Employee(
            //    employeeNumber,
            //    employeeName,
            //    departmentId,
            //    positionId,
            //    genderCode,
            //    salary
            //);
            //bool updatedEmployeeResult = await employeeService.UpdateAsync(employeeNumber, updatedEmployee);

            /**
           Third Objective: Apply CRUD operation in EF core on Vacation request full cycle
           as below:
1.Submit new vacation request action for employee and make sure to not
overlap vacations for same employee.
2.System show pending vacation requests should employee take actions on and
he has one of 2 options to take on as below:
a.Approve vacation request by call method: Approve
b.Decline vacation request by call method: Decline * */


            VacationRequestService vacationRequestService = new VacationRequestService(context);
            await vacationRequestService.CreateAsync(new VacationRequest()
            {
                RequestSubmissionDate = DateTime.Now, // current date/time
                Description = "Vacation for visit Jordan", // description of the vacation
                EmployeeNumber = "EMP001", // Employee number making the request
                VacationTypeCode = "A", // Assuming 'A' represents annual leave, for example
                StartDate = new DateTime(2025, 5, 1), // Start date of the vacation
                EndDate = new DateTime(2025, 5, 7), // End date of the vacation
                RequestStateId = 1, // assuming '1' represents 'Pending' state
        
            });

            var pendingRequests = await vacationRequestService.GetPendingRequestsAsync();
            Console.WriteLine("Pending Vacation Requests:\n");
            // Print out the details of each pending request

            foreach (var request in pendingRequests)
            {
                Console.WriteLine($"Request ID: {request.RequestId}");
                Console.WriteLine($"Submission Date: {request.RequestSubmissionDate}");
                Console.WriteLine($"Description: {request.Description}");
                Console.WriteLine($"Start Date: {request.StartDate}");
                Console.WriteLine($"End Date: {request.EndDate}");
                Console.WriteLine($"Total Vacation Days: {request.TotalVacationDays}");
                Console.WriteLine($"Employee Name: {request.EmployeeName}");
                Console.WriteLine($"Employee Position: {request.EmployeePosition}");
                Console.WriteLine($"Request State ID: {request.RequestStateId}");
                Console.WriteLine($"Approved By: {request.ApprovedByEmployeeNumber ?? "Not approved yet"}");
                Console.WriteLine($"Declined By: {request.DeclinedByEmployeeNumber ?? "Not declined yet"}");
                Console.WriteLine("-------------------------------------------------------------");
            }


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
