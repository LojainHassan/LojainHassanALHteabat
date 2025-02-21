using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstProject.Models.Root;

namespace MyFirstProject.Models.Entities;

public class Employee:RootModel
{ 
    // Employee number, primary key but not identity
    [Key]
    [StringLength(6)]
    [Required]
    public string EmployeeNumber { get; set; }

    // Employee name, required with a maximum length of 20 characters
    [Required]
    [StringLength(20)]
    public string EmployeeName { get; set; }

    // Foreign key for Department
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    // Foreign key for Position
    [ForeignKey("Position")]
    public int PositionId { get; set; }
    public Position Position { get; set; }

    // Gender code, required with max length 1 character
    [Required]
    [StringLength(1)]
    public string GenderCode { get; set; }

    // Reports to another employee (nullable)
    [StringLength(6)]
    public string? ReportedToEmployeeNumber { get; set; }

    // Vacation days left, max 24 days and set it deafaut 
    [Range(0, 24)]
    public int VacationDaysLeft { get; set; } = 24;

    // Salary with max 2 decimal places
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

    // Constructor to inject the database context


    private static ApplicationDbContext _context;

    // Constructor to inject the database context
    public Employee(ApplicationDbContext context)
    {
        _context = context;
    }
    // ✅ Constructor to initialize Employee without DB context (for adding employees)
    public Employee(string employeeNumber, string employeeName, int departmentId, int positionId, string genderCode, decimal salary)
    {
        EmployeeNumber = employeeNumber;
        EmployeeName = employeeName;
        DepartmentId = departmentId;
        PositionId = positionId;
        GenderCode = genderCode;
        Salary = salary;
        VacationDaysLeft = 24; // ✅ Always 24 days by default
    }
    // ✅ Add default employees method
    public async Task AddDefaultEmployees()
    {
        var defaultEmployees = new List<Employee>
        {
            new Employee("EMP001", "محمد أحمد", 1, 1, "M", 50000),
            new Employee("EMP002", "فاطمة علي", 2, 2, "F", 55000),
            new Employee("EMP003", "أحمد حسن", 1, 3, "M", 60000),
            new Employee("EMP004", "ليلى سعيد", 3, 4, "F", 48000),
            new Employee("EMP005", "يوسف محمود", 2, 5, "M", 52000),
            new Employee("EMP006", "سارة خالد", 1, 6, "F", 57000),
            new Employee("EMP007", "علي إبراهيم", 3, 7, "M", 53000),
            new Employee("EMP008", "مريم عبد الله", 2, 8, "F", 59000),
            new Employee("EMP009", "خالد مصطفى", 1, 9, "M", 56000),
            new Employee("EMP010", "هدى عبد الرحمن", 3, 10, "F", 61000)
        };
        await _context.Employees.AddRangeAsync(defaultEmployees);
        await _context.SaveChangesAsync();
    }
}

