using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Contracts.Dto
{
    public class VacationRequestDto
    {
        public int RequestId { get; set; }
        public DateTime RequestSubmissionDate { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalVacationDays { get; set; }
        public int RequestStateId { get; set; }
        public string ApprovedByEmployeeNumber { get; set; }
        public string DeclinedByEmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
    }

}
