using LinkDev.IKEA.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController:Controller
    {
        private readonly ILogger<EmployeeController> logger;
        #region Services
        private readonly IEmployeeService _employeeService;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            this.logger = logger;
            _employeeService = employeeService;
        }
        #endregion

    }
}
