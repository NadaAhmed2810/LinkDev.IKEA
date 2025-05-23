using AutoMapper;
using LinkDev.IKEA.BLL.Models_DTOS_.Employee;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LinkDev.IKEA.PL.Mapping.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {

            
            CreateMap<EmployeeDto, EmployeeViewModel>();
            CreateMap<EmployeeDetailsDto, EmployeeDetailsViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Employee.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.FullName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Employee.Age))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Employee.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Employee.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Employee.Address))
            .ForMember(dest => dest.FormattedHireDate, opt => opt.MapFrom(src => src.Employee.HireDate))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
            .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.Employee.EmployeeType))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Employee.IsActive))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code))
            .ForMember(dest => dest.DepartmentDescription, opt => opt.MapFrom(src => src.Department.Description))
            .ForMember(dest => dest.ManagerofDepartmentName, opt => opt.MapFrom(src => src.Department.Manager))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Employee.CreatedBy))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Employee.CreatedOn))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.Employee.LastModifiedBy))
            .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.Employee.LastModifiedOn));

            CreateMap<EmployeeCreateViewModel, EmployeeCreateDto>();
            CreateMap<EmployeeDto, EmployeeEditViewModel>();
            CreateMap<EmployeeEditViewModel, EmployeeUpdateDto>();




        }
    }
}
