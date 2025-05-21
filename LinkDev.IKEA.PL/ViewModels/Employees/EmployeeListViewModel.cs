namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeListViewModel
    {
        public required IEnumerable<EmployeeViewModel> Employees { get; set; } 

        public int Page  { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    }
}
