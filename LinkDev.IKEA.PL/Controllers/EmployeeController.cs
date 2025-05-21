using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> logger;
        #region Services
        private readonly IEmployeeService _employeeService;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            this.logger = logger;
            _employeeService = employeeService;
        }
        #endregion
        [HttpGet] //baseUrl/Employee/Index
        public ActionResult Index(int PageIndex = 1, int PageSize = 10)
        {
            // If Exception will can throw will use try ,catch

            var queryparams = new QueryParameters()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            var employees = _employeeService.GetPaginatedEmployees(queryparams);
            var model = new EmployeeListViewModel()
            {
                Employees = employees.Data.Select(e => new EmployeeViewModel()
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
                TotalCount = employees.TotalCount
            };
            return View(model);
        }
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
            var Message = "Department Created Successfully";
            try
            {
                var employeeToCreate = new EmployeeCreateDto(model.FirstName, model.LastName,model.Email,model.PhoneNumber, model.Address, model.DateofBirth, model.Salary, model.Gender, model.EmployeeType, model.DepartmentId);
                var created = _employeeService.CreateEmployee(employeeToCreate)>0;
                if (!created)
                    Message = "Failed to create employee";
               

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message,ex.StackTrace!.ToString());
                Message = "An Error Occurred,Please Try Again Later";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = Message;
            return RedirectToAction(nameof(Index));

        }
    }
}
