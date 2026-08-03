using EmployeeApp.Core.Models;

namespace EmployeeApp.Core.Contracts;

public interface IDepartmentRepository
{
    Task<Department?> FindByIdAsync(long id);
    Task<IEnumerable<UpdateSalaryForDepartment>> UpdateSalaryForDepartment(long departmentId, int percent);
}