using LinkDev.IKEA.BLL.Models_DTOS_.Department;

namespace LinkDev.IKEA.BLL.Models_DTOS_.Employee
{
    public record EmployeeDetailsDto(
        EmployeeDto Employee,
        DepartmentDto Department,
        int YearsOfService
        );
}
