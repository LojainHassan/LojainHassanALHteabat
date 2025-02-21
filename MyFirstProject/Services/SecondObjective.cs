using MyFirstProject.Contracts;
using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Services;

public class SecondObjective
{
    public DepartmentServices _departmentService { get; set; }

    public SecondObjective(DepartmentServices departmentService)
	{
        _departmentService = departmentService;
    }

    ////<summary>Question One</summary>
    ///Add about new 20 departments by use DB context and one save changes to
    ///database.
    
    public async Task Add20Departments()
    {
        List<Department> departments = new List<Department>
    {
        new Department { DepartmentName = "HR" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "IT" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Finance" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Marketing",CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Operations" , CreationTime = DateTime.Now,IsDeleted=false},
        new Department { DepartmentName = "Sales" ,CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Customer Support" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Engineering" , CreationTime = DateTime.Now,IsDeleted=false},
        new Department { DepartmentName = "Product Management",CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Legal" ,CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "R&D",CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Security",CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Data Science" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Design" ,CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Logistics" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Quality Assurance",CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Procurement" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Public Relations" ,CreationTime =DateTime.Now ,IsDeleted=false},
        new Department { DepartmentName = "Corporate Strategy",CreationTime =DateTime.Now ,IsDeleted=false },
        new Department { DepartmentName = "Compliance" ,CreationTime =DateTime.Now ,IsDeleted=false}
    };
        _departmentService.CreateRangeAsync(departments);

    }

}
