using MyFirstProject.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities;

public class Department:RootModel
{
    // Primary Key with auto-increment
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DepartmentId { get; set; }

    // Department name, required with a maximum length of 50 characters
    [Required]
    [StringLength(50)]
    public string DepartmentName { get; set; }
}
