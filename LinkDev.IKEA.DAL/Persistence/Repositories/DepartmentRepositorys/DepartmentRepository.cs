
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Repositories.DepartmentRepositories
{
    public class DepartmentRepository : BaseRepository<Department,int>,IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            _dbContext= dbContext;
        }
    
    }
}
