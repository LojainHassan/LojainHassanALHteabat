using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Models;

/// <summary>
/// Represents the application's database context, inheriting from DbContext.
/// This is used to interact with the database using Entity Framework Core.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of ApplicationDbContext with the specified options.
    /// </summary>
    /// <param name="options">The DbContext options for configuring the database.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <summary>
    /// Represents the Departments table in the database.
    /// </summary>
    public DbSet<Department> Departments { get; set; }

    /// <summary>
    /// Represents the Positions table in the database.
    /// </summary>
    public DbSet<Position> Positions { get; set; }

    /// <summary>
    /// Represents the Employees table in the database.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Represents the RequestStates table in the database.
    /// </summary>
    public DbSet<RequestState> RequestStates { get; set; }

    /// <summary>
    /// Represents the VacationRequests table in the database.
    /// </summary>
    public DbSet<VacationRequest> VacationRequests { get; set; }

    /// <summary>
    /// Represents the VacationTypes table in the database.
    /// </summary>
    public DbSet<VacationType> VacationTypes { get; set; }
}
