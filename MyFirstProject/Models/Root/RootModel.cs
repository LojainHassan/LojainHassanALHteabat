using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Models.Root;

public class RootModel
{
    public DateTime? CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool? IsDeleted { get; set; }
}
