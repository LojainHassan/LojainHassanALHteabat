using MyFirstProject.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities;
public class Position:RootModel
{
    // Primary Key with auto-increment
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PositionId { get; set; }

    // Position name, required with a maximum length of 30 characters
    [Required]
    [StringLength(30)]
    public string PositionName { get; set; }
}
