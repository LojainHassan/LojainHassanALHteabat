using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Services;

public class DepartmentServices
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public DepartmentServices(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new department to the system.
    /// </summary>
    /// <param name="department">The department entity to be added.</param>
    /// <returns>True if the operation is successful, otherwise false.</returns>
    public async Task<bool> CreateAsync(Department department)
    {
        try
        {
            _context.Departments.Add(department);  // Add the department to the context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors (e.g., database issues)
        }
    }

    /// <summary>
    /// Retrieves a department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department.</param>
    /// <returns>The department if found, otherwise null.</returns>
    public async Task<Department> GetByIdAsync(int id)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == id);  // Retrieve department by ID
    }

    /// <summary>
    /// Retrieves all departments in the system.
    /// </summary>
    /// <returns>A list of all departments.</returns>
    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();  // Retrieve all departments
    }

    /// <summary>
    /// Updates an existing department.
    /// </summary>
    /// <param name="id">The unique identifier of the department to be updated.</param>
    /// <param name="department">The updated department data.</param>
    /// <returns>True if the update is successful, otherwise false.</returns>
    public async Task<bool> UpdateAsync(int id, Department department)
    {
        try
        {
            var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (existingDepartment == null)
            {
                return false;  // Department not found
            }

            // Update the department fields
            existingDepartment.DepartmentName = department.DepartmentName;
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }

    /// <summary>
    /// Deletes a department from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the department to be deleted.</param>
    /// <returns>True if the deletion is successful, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return false;  // Department not found
            }

            _context.Departments.Remove(department);  // Remove the department from the context
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }
}
