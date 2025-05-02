using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentDetailsView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date of Creation")]

        public DateOnly CreationDate { get; set; }
        [Display(Name = "Created By")]

        public required string? CreatedBy { get; set; }
        [Display(Name = "Created On")]

        public DateTime CreatedOn { get; set; }
        [Display(Name = "Last Modified By")]

        public string? LastModifiedBy { get; set; }
        [Display(Name = "Last Modified On")]
        public required DateTime LastModifiedOn { get; set; }

    }
}
