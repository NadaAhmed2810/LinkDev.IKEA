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
            //Apply Sorting
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? OrderBy = null;
            OrderBy = queryParameters.SortedBy?.ToLower() switch
            {
                "name" => queryParameters.SortAscending ?
                query => query.OrderBy(E => E.FirstName).ThenBy(E => E.LastName)

                : query => query.OrderByDescending(E => E.FirstName).ThenByDescending(E => E.LastName),

                "email" => queryParameters.SortAscending ?
                query => query.OrderBy(e => e.Email) :
                query => query.OrderByDescending(e => e.Email),

                "hiredate" => queryParameters.SortAscending ?
                query => query.OrderBy(e => e.HireDate) :
                query => query.OrderByDescending(e => e.HireDate),

                "salary" => queryParameters.SortAscending ?
                query => query.OrderBy(e => e.Salary) :
                query => query.OrderByDescending(e => e.Salary),

                "status" => queryParameters.SortAscending ?
                query => query.OrderBy(e => e.IsActive) :
                query => query.OrderByDescending(e => e.IsActive),


                _ => queryParameters.SortAscending ? query => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName) :
                 query => query.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName)





            };

                
            return base.GetAll(queryParameters, filter, OrderBy, Includes);
           
        }
    }
}
