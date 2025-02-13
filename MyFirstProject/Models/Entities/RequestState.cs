using MyFirstProject.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Entities;

public class RequestState:RootModel
{
    // Primary Key: A unique identifier for each state
    [Key]
    public int StateId { get; set; }

    // State Name: A required field with a maximum length of 10 characters
    [Required]
    [StringLength(10)]  // Limits the length to 10 characters
    public string StateName { get; set; }
}
