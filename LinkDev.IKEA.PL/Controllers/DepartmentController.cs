using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is a Controller
    //Composition : DepartmentController Has a IDepartmentService
    public class DepartmentController/*(IDepartmentService departmentService)*/ : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion
        #region Index
        [HttpGet]//baseUrl/Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            var DepartmentViewModel = departments.Select(department => new DepartmentViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate
            });

            return View(DepartmentViewModel);
        }
        #endregion
        #region Details
        [HttpGet] //Get :BaseUrl/Department/Details/id
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();

            var DepartmentDetailsView = new DepartmentDetailsView()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn

            };

            return View(department);
        }

        #endregion
    }
}
