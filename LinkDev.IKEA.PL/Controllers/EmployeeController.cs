using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly ILogger<EmployeeController> logger;

        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService,IWebHostEnvironment environment)
        {
            this.logger = logger;
            _employeeService = employeeService;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] //baseUrl/Employee/Index
        public ActionResult Index(string SearchTerm="",
                                  string SortedBy="",
                                  bool SortAscending=true,
                                  int PageIndex = 1,
                                  int PageSize = 10)
        {
            // If Exception will can throw will use try ,catch

            var queryparams = new QueryParameters()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                SearchTerm = SearchTerm,
                SortedBy = SortedBy,
                SortAscending = SortAscending
            };
            var PaginatedResult = _employeeService.GetPaginatedEmployees(queryparams);
            var model = new EmployeeListViewModel()
            {
                Employees = PaginatedResult.Data.Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Age = e.Age,
                    Address = e.Address,
                    Salary = e.Salary,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    IsActive = e.IsActive,
                    FormattedHireDate = e.HireDate.ToString("dd/MM/yyyy"),
                    Gender = e.Gender,
                    EmployeeType = e.EmployeeType,
                    DepartmentName = e.DepartmentName,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,

                }),
                PageSize = PageSize,
                Page = PageIndex,
                SortedBy=SortedBy,
                SortAscending=SortAscending,
                TotalCount = PaginatedResult.TotalCount
            };
            return View(model);
        }
        #endregion
        #region Details
        [HttpGet]//baseUrl/Employee/Details/Id
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                if (!id.HasValue)
                    return BadRequest();
            var EmployeeDetails = _employeeService.GetEmployeeDetails(id.Value);
            if (EmployeeDetails == null)
                return NotFound();
            var model = new EmployeeDetailsViewModel()
            {
                Id = EmployeeDetails.Employee.Id,
                FirstName = EmployeeDetails.Employee.FirstName,
                LastName = EmployeeDetails.Employee.LastName,
                Age = EmployeeDetails.Employee.Age,
                Email = EmployeeDetails.Employee.Email,
                PhoneNumber = EmployeeDetails.Employee.PhoneNumber,
                Address = EmployeeDetails.Employee.Address,
                FormattedHireDate = EmployeeDetails.Employee.HireDate.ToString("dd/MM/yyyy"),
                Salary = EmployeeDetails.Employee.Salary,
                EmployeeType = EmployeeDetails.Employee.EmployeeType,
                IsActive = EmployeeDetails.Employee.IsActive,
                Gender = EmployeeDetails.Employee.Gender,
                YearsOfService = EmployeeDetails.YearsOfService,
                DepartmentId = EmployeeDetails.Department.Id,
                DepartmentName = EmployeeDetails.Department.Name,
                DepartmentCode = EmployeeDetails.Department.Code,
                DepartmentDescription = EmployeeDetails.Department.Description,
                CreatedBy = EmployeeDetails.Employee.CreatedBy,
                CreatedDate = EmployeeDetails.Employee.CreatedOn,
                LastModifiedBy = EmployeeDetails.Employee.LastModifiedBy,
                LastModifiedDate = EmployeeDetails.Employee.LastModifiedOn,

            };

            return View(model);
        }

        #endregion
        #region Create
        [HttpGet]//baseUrl/Employee/Create
        public ActionResult Create()
        {
            var model = new EmployeeCreateViewModel()
            {

                DateofBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-18))
            };
            return View(model);
        }

        [HttpPost]//baseUrl/Employee/Create
        public ActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //var departments = _departmentService.GetDepartments();
                //model.Departments = departments.Select(d => new SelectListItem()
                //{
                //    Value = d.Id.ToString(),
                //    Text = d.Name
                //});
                return View(model);

            }
            var message = "Department Created Successfully";
            try
            {
                var employeeToCreate = new EmployeeCreateDto(model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.Address, model.DateofBirth, model.Salary, model.Gender, model.EmployeeType, model.DepartmentId);
                var created = _employeeService.CreateEmployee(employeeToCreate) > 0;
                if (!created)
                    message = "Failed to create employee";


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (environment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An Error Occurred,Please Try Again Later";
            }
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #region Edit
        [HttpGet]//baseUrl/Employee/Edit/Id?
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var employeeDetails = _employeeService.GetEmployeeById(id.Value);
            if (employeeDetails == null)
                return NotFound();
            var model = new EmployeeEditViewModel()
            {
                Id = employeeDetails.Id,
                FirstName = employeeDetails.FirstName,
                LastName = employeeDetails.LastName,
                Email = employeeDetails.Email,
                PhoneNumber = employeeDetails.PhoneNumber,
                Address = employeeDetails.Address,
                //DateofBirth = employeeDetails.DateofBirth,
                Salary = employeeDetails.Salary,
                IsActive = employeeDetails.IsActive,
                Gender = employeeDetails.Gender,
                EmployeeType = employeeDetails.EmployeeType,
                HiringDate = employeeDetails.HireDate,
                DepartmentId = employeeDetails.DepartmentId


            };
            TempData["Id"] = id;
            return View(model);
        }

        [HttpPost]//baseUrl/Employee/Edit/Id
        public ActionResult Edit([FromRoute] int id, EmployeeEditViewModel model)
        {
            if (((int?)TempData["id"]) != id)
            {
                ModelState.AddModelError("Id", "Invalid Id ");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var message = "Employee Updated Successfully";

            try
            {
                var EmployeeToUpdate = new EmployeeUpdateDto(model.Id, model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.Address, model.Salary, model.IsActive, model.Gender, model.EmployeeType, model.DepartmentId);
                var Updated = _employeeService.UpdateEmployee(EmployeeToUpdate) > 0;
                if (!Updated)
                {
                    message = "Failed to Update Employee";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (environment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An Error Occurred,Please Try Again Later";
            }
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));


        }

        #endregion
        //Need To Update using JavaScript
        #region Delete
        public IActionResult Delete(int Id)
        {

            var message = "Department Deleted Successfully";

            try
            {
                var deleted = _employeeService.DeleteEmployee(Id) > 0;
                if (!deleted)
                    message = "Failed to Deleted Department";
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (environment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An Error Occurred,Please Try Again Later";

            }
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}
