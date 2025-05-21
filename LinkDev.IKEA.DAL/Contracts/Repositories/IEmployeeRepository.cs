using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IEmployeeRepository:IBaseRepository<Employee, int>
    {
        PaginatedResult<Employee> GetAll(QueryParameters queryParameters);


    }
}
