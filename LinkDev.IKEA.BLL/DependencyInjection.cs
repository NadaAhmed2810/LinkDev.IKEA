using System.Reflection;
using LinkDev.IKEA.BLL.Mapping.Profiles;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LinkDev.IKEA.BLL
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(M => M.AddProfiles(new[] { new EmployeeProfile() }));
            // services.AddAutoMapper(typeof(AssemblyInformation).Assembly);//if i want to access another assembly through ine class
            //services.AddAutoMapper(typeof(EmployeeProfile));

            return services;
        } 
    }
}
