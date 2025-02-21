using MyFirstProject.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities;

public class VacationType:RootModel
{
    // Primary Key, 1 character code
    [Key]
    [StringLength(1)]
    [Required]
    public string VacationTypeCode { get; set; }

    // Vacation type name, required with a maximum length of 20 characters
    [Required]
    [StringLength(20)]
    public string VacationTypeName { get; set; }
}
