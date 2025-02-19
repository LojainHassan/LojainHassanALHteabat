using MyFirstProject.Models.Entities;
using MyFirstProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}
