using LinkDev.IKEA.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts
{
   

    public interface IUnitOFWork
    {
        public IDepartmentRepository? departmentRepository { get; set; }
        void Dispose();
        int Complete();
    }
}
