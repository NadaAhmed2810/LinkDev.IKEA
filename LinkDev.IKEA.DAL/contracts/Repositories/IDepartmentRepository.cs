using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.contracts.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withTracking=false);
        Department? GetById(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(int id);

    }
}
