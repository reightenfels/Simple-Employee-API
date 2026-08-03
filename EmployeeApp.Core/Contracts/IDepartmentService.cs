using EmployeeApp.Core.DTO.Department;

namespace EmployeeApp.Core.Contracts;

public interface IDepartmentService
{
    Task<IEnumerable<UpdateSalaryResultDTO>> UpdateSalaryForDepartment(long departmentId, int percent);
    Task<bool> IsExists(long id);
}