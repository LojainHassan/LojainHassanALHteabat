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

    // Vacation days left, max 24 days
    [Range(0, 24)]
    public int VacationDaysLeft { get; set; }

    // Salary with max 2 decimal places
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }
}
