using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Enums;

namespace LinkDev.IKEA.BLL.Models_DTOS_.Employee
{
    public record EmployeeUpdateDto(
        int id,
        string FirstName,
        string LastName, 
        string? Email,
        string? PhoneNumber,
        string? Address,
        decimal Salary,
        bool IsActive,
        Gender Gender,
        EmployeeType EmployeeType,
        int? DepartmentId);
}
