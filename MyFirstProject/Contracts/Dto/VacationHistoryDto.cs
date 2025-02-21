using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Contracts.Dto
{
    public class VacationHistoryDto
    {
        public string VacationType { get; set; }
        public string Description { get; set; }
        public string RequestDuration { get; set; }
        public int TotalVacationDays { get; set; }
        public string ApprovedBy { get; set; }
    }

}
