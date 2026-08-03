using EmployeeApp.Core.DTO.Employee;
using EmployeeApp.Core.DTO.Requests;
using EmployeeApp.Core.Exceptions;
using EmployeeApp.Core.Models;
using EmployeeApp.Core.Contracts;

namespace EmployeeApp.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    private static EmployeeDTO? MapToDto(Employee employee)
    {
        if (employee == null) return null; 

        return new EmployeeDTO
        {
            Id = employee.Id,
            DepartmentName = employee.Department != null ? employee.Department.Name : "",
            ChiefName = employee.Chief != null ? employee.Chief.Name : "",
            Name = employee.Name,
            Salary = employee.Salary
        };
    }

    public async Task<IEnumerable<EmployeeDTO?>> GetAllAsync()
    {
        IEnumerable<Employee> employees = await _repository.FindAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<EmployeeDTO?> GetByIdAsync(long id)
    {
        Employee? employee = await _repository.FindByIdAsync(id) ?? throw new ResourceNotFoundException($"Не найден пользователь с ID: {id}");
        return MapToDto(employee);
    }

    public async Task<EmployeeDTO?> CreateAsync(CreateEmployeeDTO employee)
    {
        Employee newEmployee = new()
        {
            Id = employee.Id,
            DepartmentId = employee.DepartmentId,
            ChiefId = employee.ChiefId,
            Name = employee.Name,
            Salary = employee.Salary
        };

        Employee createdEmployee = await _repository.CreateAsync(newEmployee);
        return MapToDto(createdEmployee) ?? null;
    }

    public async Task<EmployeeDTO?> UpsertAsync(UpsertEmployeeDTO dto)
    {
        Employee? employee;
        if (dto.Id.HasValue && dto.Id.Value > 0)
        {
            employee = await _repository.FindByIdAsync((long)dto.Id) ?? throw new ResourceNotFoundException($"Не найден пользователь с ID: {dto.Id}");

            employee.DepartmentId = dto.DepartmentId;
            employee.ChiefId = dto.ChiefId;
            employee.Name = dto.Name;
            employee.Salary = dto.Salary; 

            await _repository.SaveChangesAsync();
        }
        else
        {
            employee = new()
            {
                DepartmentId = dto.DepartmentId,
                ChiefId = dto.ChiefId,
                Name = dto.Name,
                Salary = dto.Salary
            };

            await _repository.CreateAsync(employee);
        }
        
        return MapToDto(employee);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> IsExists(long id)
    {
        Employee? employee = await _repository.FindByIdAsync(id);
        return employee != null;
    }
}