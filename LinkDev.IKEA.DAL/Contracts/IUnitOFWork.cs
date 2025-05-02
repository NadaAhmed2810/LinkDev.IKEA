using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Repositories;
using LinkDev.IKEA.DAL.Persistence.Repositories.DepartmentRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts
{
    public interface IUnitOFWork
       
    {
        public IDepartmentRepository  Departments { get; }
        public  IEmployeeRepository Employees { get; }
        void Dispose();
        int Complete();
    }
}
