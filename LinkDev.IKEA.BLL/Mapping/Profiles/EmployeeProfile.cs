using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkDev.IKEA.BLL.Models_DTOS_.Department;
using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;

namespace LinkDev.IKEA.BLL.Mapping.Profiles
{
    public class EmployeeProfile:Profile 
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => $"sir.{src.FirstName}"))
                //if i want to ingore field exist in the dest and i donot want to map it
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department!.Name))
                .ForMember(dest => dest.DepartmentName, options =>
                {
                    options.Condition(src => src.Department is not null);
                    options.MapFrom(src => src.Department!.Name);
                })
                .ForMember(dest => dest.FormattedHirirngDate, options => options.MapFrom(src => src.HireDate.ToString("MMMM-dd-yyyy")))
            //If you do not use ReverseMap() you have to do not it because the mapping is make heading 

            .ReverseMap();
            //.ForMember(des=>des.FirstName,option=>option.MapFrom(src=>src.FirstName));


            CreateMap<Department, DepartmentDto>();

            CreateMap<EmployeeCreateDto, Employee>();
               
        }
            
    }
}
