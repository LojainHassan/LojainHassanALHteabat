using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstProject.Models.Root;

namespace MyFirstProject.Models.Entities;

public class VacationRequest:RootModel
{
    // Primary Key: Unique identifier for each vacation request, identity column
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RequestId { get; set; }

    // Request submission date: The date and time when the request was submitted
    [Required]
    public DateTime RequestSubmissionDate { get; set; }

    // Description: A required field with a maximum length of 100 characters
    [Required]
    [StringLength(100)]  // Limits the length to 100 characters
    public string Description { get; set; }

    // Employee number: The number identifying the employee requesting the vacation
    public string EmployeeNumber { get; set; }

    // Vacation Type code: A single character code to represent the type of vacation
    [StringLength(1)]  // Limits the length to 1 character
    public string VacationTypeCode { get; set; }

    // Start date: The start date of the vacation, required
    [Required]
    public DateTime StartDate { get; set; }

    // End date: The end date of the vacation, required
    [Required]
    public DateTime EndDate { get; set; }

    // Total vacation days: The total number of days requested, required
    [Required]
    public int TotalVacationDays { get; set; }

    // Request state Id: Foreign key linking to a state for the vacation request, required
    [Required]
    public int RequestStateId { get; set; }

    // Approved by employee number: The employee number of the one who approved the request, nullable
    public string? ApprovedByEmployeeNumber { get; set; }

    // Declined by employee number: The employee number of the one who declined the request, nullable
    public string? DeclinedByEmployeeNumber { get; set; }
}
