using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Services;

public class PositionService
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public PositionService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new position to the system.
    /// </summary>
    /// <param name="position">The position entity to be added.</param>
    /// <returns>True if the operation is successful, otherwise false.</returns>
    public async Task<bool> CreateAsync(Position position)
    {
        try
        {
            _context.Positions.Add(position);  // Add the position to the context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors (e.g., database issues)
        }
    }

    /// <summary>
    /// Retrieves a position by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the position.</param>
    /// <returns>The position if found, otherwise null.</returns>
    public async Task<Position> GetByIdAsync(int id)
    {
        return await _context.Positions
            .FirstOrDefaultAsync(p => p.PositionId == id);  // Retrieve position by ID
    }

    /// <summary>
    /// Retrieves all positions in the system.
    /// </summary>
    /// <returns>A list of all positions.</returns>
    public async Task<IEnumerable<Position>> GetAllAsync()
    {
        return await _context.Positions.ToListAsync();  // Retrieve all positions
    }

    /// <summary>
    /// Updates an existing position.
    /// </summary>
    /// <param name="id">The unique identifier of the position to be updated.</param>
    /// <param name="position">The updated position data.</param>
    /// <returns>True if the update is successful, otherwise false.</returns>
    public async Task<bool> UpdateAsync(int id, Position position)
    {
        try
        {
            var existingPosition = await _context.Positions
                .FirstOrDefaultAsync(p => p.PositionId == id);

            if (existingPosition == null)
            {
                return false;  // Position not found
            }

            // Update the position fields
            existingPosition.PositionName = position.PositionName;
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }

    /// <summary>
    /// Deletes a position from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the position to be deleted.</param>
    /// <returns>True if the deletion is successful, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var position = await _context.Positions
                .FirstOrDefaultAsync(p => p.PositionId == id);

            if (position == null)
            {
                return false;  // Position not found
            }

            _context.Positions.Remove(position);  // Remove the position from the context
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }
}
