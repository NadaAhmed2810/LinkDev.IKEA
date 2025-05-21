using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Repositories.EmployeeRepository
{
    public class EmployeeRepository: BaseRepository<Employee,int>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public PaginatedResult<Employee> GetAll(QueryParameters queryParameters)
        {
            Expression<Func<Employee, bool>>? filter = null;

            //Apply Filtering
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
               filter=e=> e.FirstName.ToLower().Contains(queryParameters.SearchTerm) || e.LastName.ToLower().Contains(queryParameters.SearchTerm);
            }
          //Apply Include
            Func<IQueryable<Employee>, IQueryable<Employee>>? Includes = null;
            Includes = e => e.Include(e => e.Department);
            return base.GetAll(queryParameters, filter, null, Includes);
        }
    }
}
