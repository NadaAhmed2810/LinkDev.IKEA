using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class CreateDepartmentView
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
