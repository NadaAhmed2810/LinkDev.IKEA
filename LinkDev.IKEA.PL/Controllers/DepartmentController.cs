using LinkDev.IKEA.BLL.Models_DTOS_.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is a Controller
    //Composition : DepartmentController Has a IDepartmentService
    public class DepartmentController/*(IDepartmentService departmentService)*/ : Controller
    {
        private readonly ILogger<DepartmentController> logger;
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(ILogger<DepartmentController> logger,IDepartmentService departmentService,IWebHostEnvironment environment)
        {
            this.logger = logger;
            _departmentService = departmentService;
            this.environment = environment;
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
        public IActionResult Details(int? id,string ViewName="Details")
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
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn

            };

            return View(ViewName,DepartmentDetailsView);
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
            if (!ModelState.IsValid)//server side validation
                return BadRequest();
            var message = "Department Created Successfully";
            try
            {
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
                if(environment.IsDevelopment())
                {
                    message = ex.Message;
                }
                else
                {
                    message = "An Error Occurred,Please Try Later ";
                }
                
            }
            TempData["Message"]=message;

            return RedirectToAction(nameof(Index));
          




        }
        #endregion
        #region Update
        [HttpGet]//Get:baseUrl/Department/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            var departmentmodel = new UpdateDepartmentViewModel() 
            { 
                Id=department.Id,
                Code =department.Code,
                Name=department.Name,
                Description=department.Description,
                CreationDate=department.CreationDate

            };
            TempData["id"] = id;
            return View(departmentmodel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int Id ,UpdateDepartmentViewModel model)
        {
            //Id Read the Value from Route 
            if ((int?)TempData["id"] != Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Id", "Invalid Error");
                return View(model);

            }
            var message = "Department Updated Successfully";
            try
            {
                var department = new UpdateDepartmentDto(Id, model.Code, model.Name, model.Description, model.CreationDate);
                var Updated = _departmentService.UpdateDepartment(department) > 0;
                if (!Updated)
                    message = "Failed to Update Department";
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (environment.IsDevelopment())
                {
                    message = ex.Message;
                }
                else
                {
                    message = "An Error Occurred,Please Try Later ";
                }
            }
            TempData["Message"]=message;
            return RedirectToAction(nameof(Index));


        }
        #endregion
        #region Delete
        [HttpGet]
        //public IActionResult Delete(int? id)
        //{

        //    return RedirectToAction(nameof(Details), new { id = id, ViewName = "Delete" });
        //}
        [HttpPost]
        public IActionResult Delete(int Id)
        {
         
            var message = "Department Deleted Successfully";

            try
            {
                var deleted= _departmentService.DeleteDepartment(Id);
                if (!deleted)
                    message = "Failed to Deleted Department";
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (environment.IsDevelopment())
                {
                    message = ex.Message;
                }
                else
                {
                    message = "An Error Occurred,Please Try Later ";
                }
            }
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
