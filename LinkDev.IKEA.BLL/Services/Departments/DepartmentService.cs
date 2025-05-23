using LinkDev.IKEA.BLL.Models_DTOS_.Department;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IUnitOFWork unitOfWork;
        public DepartmentService(IUnitOFWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department=unitOfWork.Departments.GetById(id);
            if (department == null)
                return null;
            return new DepartmentDetailsDTO
            (
                 id,
                department.Code,
                department.Name,
                department.Description,
                department.CreationDate,
                department.CreatedBy,
                department.CreatedOn,
                department.LastModifiedBy,
                department.LastModifiedOn

            );
        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
           var departments=unitOfWork.Departments.GetAll();
            foreach (var department in departments)
            {

                yield return new DepartmentDto(department.Id, department.Code, department.Name, department.Description, department.CreationDate,$"{department.Manager!.FirstName} {department.Manager!.LastName}");
            }
        }

        public int CreateDepartment(CreateDepartmentDto department)
        {
            var departmenttoCreate = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy=""

            };

            
             unitOfWork.Departments.Add(departmenttoCreate);
            return unitOfWork.Complete();
        }
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
            var CreateToUpdate = new Department()
            {
                Id=department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy = ""

            };


            unitOfWork.Departments.Update(CreateToUpdate);
            return unitOfWork.Complete();
        }

        public bool DeleteDepartment(int Id)
        {
             unitOfWork.Departments.Delete(Id);
            return unitOfWork.Complete() > 0;
        }

      
       
    }
}
