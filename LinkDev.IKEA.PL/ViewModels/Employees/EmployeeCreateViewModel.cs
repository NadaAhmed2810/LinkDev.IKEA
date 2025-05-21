using System.ComponentModel.DataAnnotations;
using LinkDev.IKEA.DAL.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MinLength(5, ErrorMessage = "First Name must be at least 5 characters long.")]
        [MaxLength(50, ErrorMessage = "First Name must be at most 50 characters long.")]
        public  string FirstName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(5, ErrorMessage = "Last Name must be at least 5 characters long.")]
        [MaxLength(50, ErrorMessage = "Last Name must be at most 50 characters long.")]
        public   string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public   string Email { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Birth of Date")]
        [DataType(DataType.Date)]
        public  DateOnly DateofBirth { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000.")]
        public  decimal Salary { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address must be like 123-Street-City-Country")]
        public  string? Address { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public  string? PhoneNumber { get; set; }

        [Display(Name = "Is Active")]
        public  bool IsActive { get; set; }
        [Display(Name = "Hiring Date")]
        public  DateOnly HiringDate { get; set; }
        public  Gender Gender { get; set; }

        [Display(Name = "Employee Type")]
        public  EmployeeType EmployeeType { get; set; }


        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        //for thr dropdown in the view
        public IEnumerable<SelectListItem>? Departments { get; set; } = new List<SelectListItem>();


    }
}
