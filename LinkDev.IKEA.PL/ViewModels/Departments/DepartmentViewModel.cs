using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [Display(Name ="Date of Creation") ]
        public DateOnly CreationDate { get; set; }

    }
}
