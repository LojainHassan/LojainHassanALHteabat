using MyFirstProject.Models.Entities;
using MyFirstProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Models.Enum;
using MyFirstProject.Contracts.Dto;

namespace MyFirstProject.Services;

public class VacationRequestService
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public VacationRequestService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new vacation request.
    /// </summary>
    public async Task<bool> CreateAsync(VacationRequest vacationRequest)
    {
        try
        {
            _context.VacationRequests.Add(vacationRequest);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Retrieves a vacation request by its unique ID.
    /// </summary>
    public async Task<VacationRequest> GetByIdAsync(int requestId)
    {
        return await _context.VacationRequests
            .Include(vr => vr.RequestStateId)  // Include related RequestState data if needed
            .FirstOrDefaultAsync(vr => vr.RequestId == requestId);
    }

    /// <summary>
    /// Retrieves all vacation requests.
    /// </summary>
    public async Task<IEnumerable<VacationRequest>> GetAllAsync()
    {
        return await _context.VacationRequests.ToListAsync();
    }

    /// <summary>
    /// Updates an existing vacation request.
    /// </summary>
    public async Task<bool> UpdateAsync(int requestId, VacationRequest vacationRequest)
    {
        try
        {
            var existingRequest = await _context.VacationRequests
                .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

            if (existingRequest == null)
            {
                return false;
            }

            // Update properties of the existing request
            existingRequest.RequestSubmissionDate = vacationRequest.RequestSubmissionDate;
            existingRequest.Description = vacationRequest.Description;
            existingRequest.EmployeeNumber = vacationRequest.EmployeeNumber;
            existingRequest.VacationTypeCode = vacationRequest.VacationTypeCode;
            existingRequest.StartDate = vacationRequest.StartDate;
            existingRequest.EndDate = vacationRequest.EndDate;
            existingRequest.TotalVacationDays = vacationRequest.TotalVacationDays;
            existingRequest.RequestStateId = vacationRequest.RequestStateId;
            existingRequest.ApprovedByEmployeeNumber = vacationRequest.ApprovedByEmployeeNumber;
            existingRequest.DeclinedByEmployeeNumber = vacationRequest.DeclinedByEmployeeNumber;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a vacation request.
    /// </summary>
    public async Task<bool> DeleteAsync(int requestId)
    {
        try
        {
            var vacationRequest = await _context.VacationRequests
                .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

            if (vacationRequest == null)
            {
                return false;
            }

            _context.VacationRequests.Remove(vacationRequest);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> SubmitVacationRequestAsync(VacationRequest vacationRequest)
    {
        // Check if there are any overlapping vacation requests for the same employee
        var overlappingRequest = await _context.VacationRequests
            .Where(v => v.EmployeeNumber == vacationRequest.EmployeeNumber
                        && v.StartDate < vacationRequest.EndDate
                        && v.EndDate > vacationRequest.StartDate
                        && v.RequestStateId != (int)RequestStateEnum.Approved)
            .FirstOrDefaultAsync();

        if (overlappingRequest != null)
        {
            return false;  // There is an overlapping vacation request
        }

        // Save the new vacation request if no overlap
        _context.VacationRequests.Add(vacationRequest);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> CreateRangeAsync(IEnumerable<VacationRequest> vacationRequests)
    {
        try
        {
            await _context.VacationRequests.AddRangeAsync(vacationRequests);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ApproveVacationRequestAsync(int requestId)
    {
        
            return false;
        
    }

    public async Task<bool> DeclineVacationRequestAsync(int requestId)
    {
       
            return false;
        
    }
    // Method to get all pending vacation requests
    public async Task<IEnumerable<VacationRequestDto>> GetPendingRequestsAsync()
    {
        var result = await (from vacationRequest in _context.VacationRequests
                            join employee in _context.Employees on vacationRequest.EmployeeNumber equals employee.EmployeeNumber
                            where vacationRequest.RequestStateId == 1 // Assuming 1 represents "Pending"
                            select new VacationRequestDto
                            {
                                RequestId = vacationRequest.RequestId,
                                RequestSubmissionDate = vacationRequest.RequestSubmissionDate,
                                Description = vacationRequest.Description,
                                StartDate = vacationRequest.StartDate,
                                EndDate = vacationRequest.EndDate,
                                TotalVacationDays = vacationRequest.TotalVacationDays,
                                RequestStateId = vacationRequest.RequestStateId,
                                ApprovedByEmployeeNumber = vacationRequest.ApprovedByEmployeeNumber,
                                DeclinedByEmployeeNumber = vacationRequest.DeclinedByEmployeeNumber,
                                EmployeeName = employee.EmployeeName, // You can select other employee properties here
                                EmployeePosition = employee.Position.PositionName // Assuming Position is a related entity
                            }).ToListAsync();

        return result;
    }


}
