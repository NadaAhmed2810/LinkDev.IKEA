using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories.DepartmentRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOFWork
    {
        private readonly ApplicationDbContext _context;
        public IDepartmentRepository departmentRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
           departmentRepository =new DepartmentRepository(dbContext);
        }
        public int Complete()
        {
            return _context.SaveChanges();
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
