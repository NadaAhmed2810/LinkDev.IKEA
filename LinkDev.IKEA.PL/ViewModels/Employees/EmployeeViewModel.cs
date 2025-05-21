using System.ComponentModel.DataAnnotations;
using LinkDev.IKEA.DAL.Common.Enums;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }

        [Display(Name = "Hiring Date")]
        public string? FormattedHireDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public  bool IsActive { get; set; }
        public string? Email  { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public  Gender Gender { get; set; }
        public  EmployeeType EmployeeType { get; set; }
        public  string?  DepartmentName { get; set; }
        #region Adminstration

        public  string? CreatedBy{ get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }


        #endregion



    }
}
