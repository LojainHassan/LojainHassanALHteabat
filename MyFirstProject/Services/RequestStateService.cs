using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Services;

public class RequestStateService
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public RequestStateService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new request state to the system.
    /// </summary>
    /// <param name="requestState">The request state entity to be added.</param>
    /// <returns>True if the operation is successful, otherwise false.</returns>
    public async Task<bool> CreateAsync(RequestState requestState)
    {
        try
        {
            _context.RequestStates.Add(requestState);  // Add the request state to the context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors (e.g., database issues)
        }
    }

    /// <summary>
    /// Retrieves a request state by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the request state.</param>
    /// <returns>The request state if found, otherwise null.</returns>
    public async Task<RequestState> GetByIdAsync(int id)
    {
        return await _context.RequestStates
            .FirstOrDefaultAsync(rs => rs.StateId == id);  // Retrieve request state by ID
    }

    /// <summary>
    /// Retrieves all request states in the system.
    /// </summary>
    /// <returns>A list of all request states.</returns>
    public async Task<IEnumerable<RequestState>> GetAllAsync()
    {
        return await _context.RequestStates.ToListAsync();  // Retrieve all request states
    }

    /// <summary>
    /// Updates an existing request state.
    /// </summary>
    /// <param name="id">The unique identifier of the request state to be updated.</param>
    /// <param name="requestState">The updated request state data.</param>
    /// <returns>True if the update is successful, otherwise false.</returns>
    public async Task<bool> UpdateAsync(int id, RequestState requestState)
    {
        try
        {
            var existingState = await _context.RequestStates
                .FirstOrDefaultAsync(rs => rs.StateId == id);

            if (existingState == null)
            {
                return false;  // Request state not found
            }

            // Update the request state fields
            existingState.StateName = requestState.StateName;
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }

    /// <summary>
    /// Deletes a request state from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the request state to be deleted.</param>
    /// <returns>True if the deletion is successful, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var requestState = await _context.RequestStates
                .FirstOrDefaultAsync(rs => rs.StateId == id);

            if (requestState == null)
            {
                return false;  // Request state not found
            }

            _context.RequestStates.Remove(requestState);  // Remove the request state from the context
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
        catch (Exception)
        {
            return false;  // Handle errors
        }
    }

    public async Task<bool> CreateRangeAsync(IEnumerable<RequestState> requestStates)
    {
        try
        {
            await _context.RequestStates.AddRangeAsync(requestStates);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
