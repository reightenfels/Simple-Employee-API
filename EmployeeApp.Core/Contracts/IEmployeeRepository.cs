using EmployeeApp.Core.Models;

namespace EmployeeApp.Core.Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> FindAllAsync();
    Task<Employee?> FindByIdAsync(long id);
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> DeleteAsync(long id);
    Task SaveChangesAsync();
}