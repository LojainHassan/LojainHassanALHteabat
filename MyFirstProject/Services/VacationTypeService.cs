using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Services;

public class VacationTypeService
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public VacationTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new vacation type to the system.
    /// </summary>
    /// <param name="vacationType">The vacation type entity to be added.</param>
    /// <returns>True if the operation is successful, otherwise false.</returns>
    public async Task<bool> CreateAsync(VacationType vacationType)
    {
        try
        {
            _context.VacationTypes.Add(vacationType);  // Add the vacation type to the context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors (e.g., database issues)
        }
    }

    /// <summary>
    /// Retrieves a vacation type by its unique code.
    /// </summary>
    /// <param name="code">The unique vacation type code.</param>
    /// <returns>The vacation type if found, otherwise null.</returns>
    public async Task<VacationType> GetByCodeAsync(string code)
    {
        return await _context.VacationTypes
            .FirstOrDefaultAsync(vt => vt.VacationTypeCode == code);  // Retrieve vacation type by code
    }

    /// <summary>
    /// Retrieves all vacation types in the system.
    /// </summary>
    /// <returns>A list of all vacation types.</returns>
    public async Task<IEnumerable<VacationType>> GetAllAsync()
    {
        return await _context.VacationTypes.ToListAsync();  // Retrieve all vacation types
    }

    /// <summary>
    /// Updates an existing vacation type.
    /// </summary>
    /// <param name="code">The vacation type code of the vacation type to be updated.</param>
    /// <param name="vacationType">The updated vacation type data.</param>
    /// <returns>True if the update is successful, otherwise false.</returns>
    public async Task<bool> UpdateAsync(string code, VacationType vacationType)
    {
        try
        {
            var existingVacationType = await _context.VacationTypes
                .FirstOrDefaultAsync(vt => vt.VacationTypeCode == code);

            if (existingVacationType == null)
            {
                return false;  // Vacation type not found
            }

            // Update the vacation type fields
            existingVacationType.VacationTypeName = vacationType.VacationTypeName;
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }

    /// <summary>
    /// Deletes a vacation type from the system.
    /// </summary>
    /// <param name="code">The vacation type code of the vacation type to be deleted.</param>
    /// <returns>True if the deletion is successful, otherwise false.</returns>
    public async Task<bool> DeleteAsync(string code)
    {
        try
        {
            var vacationType = await _context.VacationTypes
                .FirstOrDefaultAsync(vt => vt.VacationTypeCode == code);

            if (vacationType == null)
            {
                return false;  // Vacation type not found
            }

            _context.VacationTypes.Remove(vacationType);  // Remove the vacation type from the context
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }
}
