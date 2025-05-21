using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.DAL.Persistence.Common;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        EmployeeDto? GetEmployeeById(int id);
        EmployeeDetailsDto? GetEmployeeDetails(int id);
        PaginatedResult<EmployeeDto> GetPaginatedEmployees(QueryParameters queryParameters);
        int CreateEmployee(EmployeeCreateDto employee);

        void UpdateEmployee(EmployeeUpdateDto employee);
        void DeleteEmployee(int id);

    }
}
