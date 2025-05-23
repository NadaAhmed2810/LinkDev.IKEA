using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUnitOFWork _unitOfWork;
        public EmployeeService(IMapper mapper,IUnitOFWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
                return null;
            //var employeedto= new EmployeeDto(
            //     id,
            //     employee.FirstName,
            //     employee.LastName,
            //     employee.Age,
            //     employee.Email,
            //     employee.PhoneNumber,
            //     employee.Address,
            //     employee.Salary,
            //     employee.IsActive,
            //     employee.HireDate,
            //     employee.Gender,
            //     employee.EmployeeType,
            //     employee.DepartmentId,
            //     employee.Department?.Name,
            //     employee.CreatedBy,
            //     employee.CreatedOn,
            //     employee.LastModifiedBy,
            //     employee.LastModifiedOn
            //     );

            //Another Way to Map the Employee to EmployeeDto 
            //employeeDto=_mapper.Map<Employee,EmployeeDto>(employee);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public EmployeeDetailsDto? GetEmployeeDetails(int id)
        {
            //refactor it 
            var employee = _unitOfWork.Employees.Get(
                filter: E => E.Id == id,
                Include: E => E.Include(E => E.Department
                ));
            if (employee == null)
                return null;

            var employeeDetails = _mapper.Map<EmployeeDto>(employee);

            DepartmentDto departmentDto=_mapper.Map<DepartmentDto>(employee.Department);


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

            #region note
            //_unitOfWork.Employees.GetAll(queryParameters: queryParameters
            //Must be in Data Access Layer Not in Business Logic Layer 
            //Includes: E => E.Include(E => E.Department),
            //filter: E => E.FirstName.Contains(queryParameters.SearchTerm!)|| E.LastName.Contains(queryParameters.SearchTerm!)
            //orderby: E =>E.Age
            //);
            #endregion

            var employees = _unitOfWork.Employees.GetAll(queryParameters: queryParameters);
           
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees.Data);

            var result = new PaginatedResult<EmployeeDto>


            {
                Data =employeeDtos,
                PageIndex = employees.PageIndex,
                PageSize = employees.PageSize,
                TotalCount = employees.TotalCount,
            };
            return result;

        }


        public int CreateEmployee(EmployeeCreateDto employeedto)
        {
            ValidateEmployeeUpdateBusinesdRuleds(employeedto);
            var employee=_mapper.Map<Employee>(employeedto);
            ///var employee = new Employee()
            ///{
            ///    FirstName = employeedto.FirstName,
            ///    LastName = employeedto.LastName,
            ///    Email = employeedto.Email,
            ///   PhoneNumber = employeedto.PhoneNumber,
            ///    Address = employeedto.Address,
            ///    Age = DateTime.Now.Year - employeedto.BirthDate.Year,
            ///    DepartmentId = employeedto.DepartmentId,
            ///    IsActive = true,
            ///   Gender=employeedto.Gender,
            ///   EmployeeType = employeedto.EmployeeType,
            ///   Salary = employeedto.Salary,
            ///    CreatedBy = "",
            ///    LastModifiedBy = "",
            ///};
            employee.HireDate = DateOnly.FromDateTime( DateTime.Now);
        
            _unitOfWork.Employees.Add(employee);
            return _unitOfWork.Complete();
        }
        

        public int UpdateEmployee(EmployeeUpdateDto employeedto)
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
            return _unitOfWork.Complete();
        }

      

        public int DeleteEmployee(int id)
        {
            _unitOfWork.Employees.Delete(id);
            return _unitOfWork.Complete();
        }
        public bool ChangeEmployeeStatus(int id, bool IsActive)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee is null)
                throw new Exception($"Employee with ID {id} does not exist.");
            if (employee.IsActive == IsActive)
                throw new Exception($"Employee with ID {id}is already {(IsActive?"active":"Inactive")}.");
            employee.IsActive = IsActive;
            _unitOfWork.Employees.Update(employee);
            return _unitOfWork.Complete() > 0;

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
                var department = _unitOfWork.Departments.GetById(employeedto.DepartmentId!.Value);
                if (department == null)
                    throw new Exception($"Department with ID {employeedto.DepartmentId} not found");
                
            }

            if (employee.Salary < employeedto.Salary)
                throw new Exception($"Employee Salary must be at least {employee.Salary}.");


        }

    

        #endregion
    }
}
