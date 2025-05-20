using LinkDev.IKEA.BLL.Models_DTOS_.Department;
using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOFWork _unitOfWork;
        public EmployeeService(IUnitOFWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
                return null;
            return new EmployeeDto(
                 id,
                 employee.FirstName,
                 employee.LastName,
                 employee.Age,
                 employee.Email,
                 employee.PhoneNumber,
                 employee.Address,
                 employee.IsActive,
                 employee.HireDate,
                 employee.Gender,
                 employee.EmployeeType,
                 employee.DepartmentId,
                 employee.CreatedBy,
                 employee.CreatedOn,
                 employee.LastModifiedBy,
                 employee.LastModifiedOn
                 );
        }
        public EmployeeDetailsDto? GetEmployeeDetails(int id)
        {
            var employee = _unitOfWork.Employees.Get(
                filter: E => E.Id == id,
                Include: E => E.Include(E => E.Department
                ));
            if (employee == null)
                return null;

            var employeeDetails = new EmployeeDto(
                 id,
                 employee.FirstName,
                 employee.LastName,
                 employee.Age,
                 employee.Email,
                 employee.PhoneNumber,
                 employee.Address,
                 employee.IsActive,
                 employee.HireDate,
                 employee.Gender,
                 employee.EmployeeType,
                 employee.DepartmentId,
                 employee.CreatedBy,
                 employee.CreatedOn,
                 employee.LastModifiedBy,
                 employee.LastModifiedOn
                 );
            DepartmentDto departmentDto = default!;
            if (employee.Department is { })
                departmentDto = new DepartmentDto(
                employee.Department.Id,
                employee.Department.Code,
                employee.Department.Name,
                employee.Department.Description,
                employee.Department.CreationDate
                );
            var yearsOfExperience = DateTime.Now.Year - employee.HireDate.Year;

            return new EmployeeDetailsDto(
                employeeDetails,
                departmentDto,
                yearsOfExperience
                );
        }

        public PaginatedResult<EmployeeDto> GetPaginatedEmployees(QueryParameters queryParameters)
        {
            if (queryParameters.PageIndex < 1)
                queryParameters.PageIndex = 1;

            if (queryParameters.PageSize < 1)
                queryParameters.PageSize = 10;

            if (queryParameters.PageSize > 100)
                queryParameters.PageSize = 100;

            var employees = _unitOfWork.Employees.GetAll(
                queryParameters: queryParameters,
                Includes: E => E.Include(E => E.Department)
                //filter: E => E.FirstName.Contains(queryParameters.SearchTerm)== true,
                //orderby: E =>E.Age

                );
            var result = new PaginatedResult<EmployeeDto>
            {
                Data = employees.Data.Select(E => new EmployeeDto(E.Id, E.FirstName, E.LastName, E.Age, E.Email, E.PhoneNumber, E.Address, E.IsActive, E.HireDate, E.Gender, E.EmployeeType, E.DepartmentId, E.CreatedBy, E.CreatedOn, E.LastModifiedBy, E.LastModifiedOn)),
                PageIndex = employees.PageIndex,
                PageSize = employees.PageSize,
                TotalCount = employees.TotalCount,
            };
            return result;

        }


        public void CreateEmployee(EmployeeCreateDto employeedto)
        {
            ValidateEmployeeUpdateBusinesdRuleds(employeedto);
            var employee = new Employee()
            {
                FirstName = employeedto.FirstName,
                LastName = employeedto.LastName,
                Email = employeedto.Email,
                PhoneNumber = employeedto.PhoneNumber,
                Address = employeedto.Address,
                Age = DateTime.Now.Year - employeedto.BirthDate.Year,
                DepartmentId = employeedto.DepartmentId,
                IsActive = true,
                Gender=employeedto.Gender,
                EmployeeType = employeedto.EmployeeType,
                Salary = employeedto.Salary,
                CreatedBy = "",
                LastModifiedBy = "",
            };
            employee.HireDate = DateOnly.FromDateTime( DateTime.Now);
        
            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();
        }
        

        public void UpdateEmployee(EmployeeUpdateDto employeedto)
        {
            var employee = _unitOfWork.Employees.GetById(employeedto.id);
            if (employee == null)
                throw new Exception($"Employee with ID {employeedto.id} not found");
            ValidateEmployeeCreateBusinesdRules(employee,employeedto);
            employee.FirstName = employeedto.FirstName;
            employee.LastName = employeedto.LastName;
            employee.Email = employeedto.Email;
            employee.PhoneNumber = employeedto.PhoneNumber;
            employee.Address = employeedto.Address;
            employee.Salary = employeedto.Salary;
            employee.IsActive= employeedto.IsActive;
            employee.Gender = employeedto.Gender;
            employee.EmployeeType = employeedto.EmployeeType;
            employee.DepartmentId = employeedto.DepartmentId;
            _unitOfWork.Employees.Update(employee);
            _unitOfWork.Complete();
        }

      

        public void DeleteEmployee(int id)
        {
            _unitOfWork.Employees.Delete(id);
            _unitOfWork.Complete();
        }


        #region HelperMethods
        private void ValidateEmployeeUpdateBusinesdRuleds(EmployeeCreateDto employee)
        {
            if (employee.DepartmentId.HasValue)
            {
                var department = _unitOfWork.Departments.GetById(employee.DepartmentId.Value);
                if (department == null)
                {
                    throw new Exception($"Department with ID {employee.DepartmentId} not found");
                }
            }
            int minAge = 18;
            var Age = DateTime.Now.Year - employee.BirthDate.Year;
            if (Age < minAge)
            {
                throw new Exception($"Employee age must be at least {minAge} Years.Current age is {Age}");
            }

            if (employee.Salary < 5000)
                throw new Exception($"Employee Salary must be at least 5000. Current Salary is {employee.Salary}");

        }
        private void ValidateEmployeeCreateBusinesdRules(Employee employee,EmployeeUpdateDto employeedto)
        {
            if (employee.DepartmentId.HasValue)
            {
                var department = _unitOfWork.Departments.GetById(employeedto.DepartmentId.Value);
                if (department == null)
                    throw new Exception($"Department with ID {employeedto.DepartmentId} not found");
                
            }

            if (employee.Salary < employeedto.Salary)
                throw new Exception($"Employee Salary must be at least {employee.Salary}.");


        }

        #endregion
    }
}
