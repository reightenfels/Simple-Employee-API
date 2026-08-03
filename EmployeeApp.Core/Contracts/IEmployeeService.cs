using EmployeeApp.Core.DTO.Employee;
using EmployeeApp.Core.DTO.Requests;

namespace EmployeeApp.Core.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO?>> GetAllAsync();
    Task<EmployeeDTO?> GetByIdAsync(long id);
    Task<EmployeeDTO?> CreateAsync(CreateEmployeeDTO employee);
    Task<EmployeeDTO?> UpsertAsync(UpsertEmployeeDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> IsExists(long id);
}