using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Contracts.Dto
{
    public class EmployeeDto
    {
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
    }

}
