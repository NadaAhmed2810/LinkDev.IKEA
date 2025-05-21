using System.ComponentModel.DataAnnotations;
using LinkDev.IKEA.DAL.Common.Enums;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeDetailsViewModel
    {
        public  int Id { get; set; }
        [Display(Name ="First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int? Age { get; set; }
        public  string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        //Details
        [Display(Name = "Hiring Date")]
        public string? FormattedHireDate { get; set; }
        
        [Display(Name = "Years Of Service")]
        public  int YearsOfService { get; set; }
        public  Gender  Gender{ get; set; }

        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Salay")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string? DepartmentName { get; set; }
        [Display(Name = "Department Code")]
        public string? DepartmentCode { get; set; }
        [Display(Name = "Department Description")]
        public string? DepartmentDescription{ get; set; }

        [Display(Name = "Manager Of")]
        public int? ManagerofDepartmentId { get; set; }
        [Display(Name = "Manager Of Department")]
        public string? ManagerofDepartmentName { get; set; }

        //Audit information
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate{ get; set; }
        [Display(Name = "Last Modified By")]
        public string? LastModifiedBy { get; set; }
        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }


    }
}
