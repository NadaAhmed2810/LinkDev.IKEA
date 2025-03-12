
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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public Department? GetById(int id)
        {
            // var department= _dbContext.Departments.Local.FirstOrDefault(D=>D.Id==id);
            //if(department == null)
            //{
            //    _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            //}
            //return department;
            //return _dbContext.Find<Department>(id);//3.1 feature

            return _dbContext.Departments.Find(id);
        }
        public IEnumerable<Department> GetAll(bool withTracking=false)
        {
            return _dbContext.Departments.ToList();
        }
        public void Add(Department entity)=>_dbContext.Departments.Add(entity);
        
        public void Update(Department entity)=> _dbContext.Departments.Update(entity);
        
        public void Delete(int id)
        {
            var department = _dbContext.Departments.Find(id);
            if (department != null)
            { //department is {}
                _dbContext.Departments.Remove(department);
            }

        }

     

       

   
    }
}
