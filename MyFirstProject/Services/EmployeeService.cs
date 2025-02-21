using MyFirstProject.Models.Entities;
using MyFirstProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstProject.Contracts.Dto;

namespace MyFirstProject.Services;

public class EmployeeService
{
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    public async Task<bool> CreateAsync(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Retrieves an employee by their employee number.
    /// </summary>
    public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
    {
        return await _context.Employees
            .Include(e => e.Department)  // Include related Department data
            .Include(e => e.Position)    // Include related Position data
            .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
    }

    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)  // Include related Department data
            .Include(e => e.Position)    // Include related Position data
            .ToListAsync();
    }

    /// <summary>
    /// Updates an existing employee's details.
    /// </summary>
    public async Task<bool> UpdateAsync(string employeeNumber, Employee employee)
    {
        try
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);

            if (existingEmployee == null)
            {
                return false;
            }

            // Update properties of the existing employee
            existingEmployee.EmployeeName = employee.EmployeeName;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.PositionId = employee.PositionId;
            existingEmployee.GenderCode = employee.GenderCode;
            existingEmployee.ReportedToEmployeeNumber = employee.ReportedToEmployeeNumber;
            existingEmployee.VacationDaysLeft = employee.VacationDaysLeft;
            existingEmployee.Salary = employee.Salary;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes an employee.
    /// </summary>
    public async Task<bool> DeleteAsync(string employeeNumber)
    {
        try
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> CreateRangeAsync(IEnumerable<Employee> employees)
    {
        try
        {
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Updates the employee's vacation days after approving a vacation request.
    /// </summary>
    /// <param name="employeeNumber">The employee's unique number.</param>
    /// <param name="vacationDaysRequested">The number of vacation days requested to be approved.</param>
    /// <returns>A boolean indicating whether the update was successful.</returns>
    public async Task<bool> UpdateVacationDaysAsync(string employeeNumber, int vacationDaysRequested)
    {
        try
        {
            // Find the employee by employee number
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);

            if (employee == null)
            {
                return false; // Employee not found
            }

            // Check if the employee has enough vacation days left
            if (employee.VacationDaysLeft < vacationDaysRequested)
            {
                return false; // Not enough vacation days
            }

            // Decrease the vacation days balance
            employee.VacationDaysLeft -= vacationDaysRequested;

            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false; // Handle any exceptions here
        }
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesWithDetailsAsync(int take, int offset)
    {
        return await _context.Employees
            .Include(e => e.Department)  // Include related Department data
            .OrderBy(e => e.EmployeeNumber) // Ensure consistent ordering
            .Skip(offset) // Skip 'offset' number of records
            .Take(take) // Take 'take' number of records
            .Select(e => new EmployeeDto
            {
                EmployeeNumber = e.EmployeeNumber,
                FullName = e.EmployeeName, // Assuming you have EmployeeName
                DepartmentName = e.Department != null ? e.Department.DepartmentName : string.Empty, // Handle null safely
                Salary = e.Salary
            })
            .ToListAsync();
    }

    public async Task<EmployeeDetailsDto> GetEmployeeByNumberAsync(string employeeNumber)
    {
        var result = await (from e in _context.Employees
                            join d in _context.Departments on e.DepartmentId equals d.DepartmentId
                            join p in _context.Positions on e.PositionId equals p.PositionId
                            join r in _context.Employees on e.ReportedToEmployeeNumber equals r.EmployeeNumber into reporting
                            from manager in reporting.DefaultIfEmpty() // Left join to include employees without a manager
                            where e.EmployeeNumber == employeeNumber
                            select new EmployeeDetailsDto
                            {
                                EmployeeNumber = e.EmployeeNumber,
                                FullName = e.EmployeeName,
                                DepartmentName = d.DepartmentName,
                                PositionName = p.PositionName,
                                ReportedToEmployeeName = manager != null ? manager.EmployeeName : String.Empty,
                                TotalVacationDaysLeft = e.VacationDaysLeft
                            }).FirstOrDefaultAsync();

        return result;
    }
    public async Task<List<EmployeeDetailsDto>> GetEmployeesWithPendingVacationsAsync()
    {
        var result = await (from e in _context.Employees
                            join v in _context.VacationRequests
                                on e.EmployeeNumber equals v.EmployeeNumber
                            where v.RequestStateId == 1  // Pending requests
                            select new EmployeeDetailsDto
                            {
                                EmployeeNumber = e.EmployeeNumber,
                                FullName = e.EmployeeName,
                                DepartmentName = e.Department.DepartmentName,
                                PositionName = e.Position.PositionName,
                                TotalVacationDaysLeft = e.VacationDaysLeft
                            })
                            .Distinct() // Ensure each employee appears only once
                            .ToListAsync();

        return result;
    }

    public async Task<List<VacationHistoryDto>> GetApprovedVacationRequestsHistoryAsync(string employeeNumber)
    {
        var result = await (from v in _context.VacationRequests
                            join e in _context.Employees
                                on v.EmployeeNumber equals e.EmployeeNumber
                            join approver in _context.Employees
                                on v.ApprovedByEmployeeNumber equals approver.EmployeeNumber
                            where v.EmployeeNumber == employeeNumber
                                  && v.RequestStateId == 2  // Approved requests
                            select new VacationHistoryDto
                            {
                                VacationType = v.VacationTypeCode,
                                Description = v.Description,
                                RequestDuration = $"{v.StartDate.ToString("yyyy-MM-dd")} to {v.EndDate.ToString("yyyy-MM-dd")}",
                                TotalVacationDays = v.TotalVacationDays,
                                ApprovedBy = approver.EmployeeName
                            })
                            .ToListAsync();

        return result;
    }

    public async Task<List<PendingVacationRequestDto>> GetPendingVacationRequestsAsync(string employeeNumber)
    {
        var result = await (from v in _context.VacationRequests
                            join e in _context.Employees
                                on v.EmployeeNumber equals e.EmployeeNumber
                            where v.RequestStateId == 1  // Pending requests
                                  && v.ApprovedByEmployeeNumber == null // Only requests that need action
                            select new PendingVacationRequestDto
                            {
                                Description = v.Description,
                                EmployeeNumber = e.EmployeeNumber,
                                EmployeeName = e.EmployeeName,
                                SubmittedOn = v.RequestSubmissionDate.ToString("yyyy-MM-dd"),
                                VacationDuration = $"{(v.EndDate - v.StartDate).Days} days",
                                StartDate = v.StartDate.ToString("yyyy-MM-dd"),
                                EndDate = v.EndDate.ToString("yyyy-MM-dd"),
                                EmployeeSalary = e.Salary
                            })
                            .ToListAsync();

        return result;
    }



}
