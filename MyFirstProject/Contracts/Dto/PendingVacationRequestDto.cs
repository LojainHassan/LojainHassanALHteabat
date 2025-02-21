using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Contracts.Dto
{
    public class PendingVacationRequestDto
    {
        public string Description { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string SubmittedOn { get; set; }
        public string VacationDuration { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal EmployeeSalary { get; set; }
    }
}
