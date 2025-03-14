using LinkDev.IKEA.BLL.Models_DTOS_.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is a Controller
    //Composition : DepartmentController Has a IDepartmentService
    public class DepartmentController/*(IDepartmentService departmentService)*/ : Controller
    {
        private readonly ILogger<DepartmentController> logger;
        #region Services
        private readonly IDepartmentService _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger,IDepartmentService departmentService)
        {
            this.logger = logger;
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

            return View(DepartmentDetailsView);
        }

        #endregion
        #region Create
        [HttpGet]//Get:BaseUrl/Department/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//Post:BaseUrl/Department/Create
        public IActionResult Create(CreateDepartmentView model)
        {
            var message = "Department Created Successfully";
            try
            {
                if (!ModelState.IsValid)//server side validation
                    return BadRequest();
                var department = new CreateDepartmentDto(model.Code, model.Name, model.Description, model.CreationDate);
                var Created = _departmentService.CreateDepartment(department) > 0;
                if (!Created)
                    message = "Failed To Create Department";

            }
            catch (Exception ex)
            {
                //1.Log Exception in Database or External File [By using Serial Package]
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                //2.Set message
                message = "An Erroe Occured,Please Try Later ";
                throw;
            }
            TempData["Message"]=message;

            return RedirectToAction(nameof(Index));
          




        }
        #endregion
    }
}
