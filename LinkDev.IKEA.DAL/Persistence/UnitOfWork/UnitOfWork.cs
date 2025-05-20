using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories.DepartmentRepositories;
using LinkDev.IKEA.DAL.Persistence.Repositories.EmployeeRepository;

namespace LinkDev.IKEA.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOFWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<DepartmentRepository> _departmentRepository;
        private readonly Lazy<EmployeeRepository> _employeeRepository;
        public IDepartmentRepository departmentRepository { get; set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _departmentRepository = new Lazy<DepartmentRepository>(()=>new DepartmentRepository(_context));
            _employeeRepository = new Lazy<EmployeeRepository>(()=>new EmployeeRepository(_context));
        }
        public IDepartmentRepository Departments => _departmentRepository.Value;
        public IEmployeeRepository Employees => _employeeRepository.Value;


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
