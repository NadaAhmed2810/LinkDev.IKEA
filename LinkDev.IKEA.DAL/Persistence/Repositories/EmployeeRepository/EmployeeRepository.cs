using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;

namespace LinkDev.IKEA.DAL.Persistence.Repositories.EmployeeRepository
{
    public class EmployeeRepository: BaseRepository<Employee,int>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
