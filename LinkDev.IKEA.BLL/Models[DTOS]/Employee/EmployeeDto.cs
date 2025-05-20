using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Enums;

namespace LinkDev.IKEA.BLL.Models_DTOS_.Employee
{
    public record EmployeeDto
    (
         int Id,
         string FirstName,
         string LastName,
         int? Age,
         string? Email,
         string? PhoneNumber,
         string? Address,
         bool IsActive,
         DateOnly HireDate,
         Gender Gender,
         EmployeeType EmployeeType,
         int? DepartmentId,
         string? CreatedBy,
         DateTime CreatedOn,
         string? LastModifiedBy,
         DateTime LastModifiedOn
    )
    { 
        public string FullName => $"{FirstName} {LastName}";
        public string FormattedHirirngDate => HireDate.ToString("MMMM-dd-yyyy");
    }

}
