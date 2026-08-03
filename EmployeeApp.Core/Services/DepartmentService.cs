

using EmployeeApp.Core.DTO.Department;
using EmployeeApp.Core.Contracts;
using EmployeeApp.Core.Models;
using EmployeeApp.Core.Exceptions;

namespace EmployeeApp.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    private static UpdateSalaryResultDTO MapToUpdateSalaryResultDto(UpdateSalaryForDepartment model)
    {
        return new UpdateSalaryResultDTO()
        {
            Id = model.Id,
            DepartmentId = model.DepartmentId,
            ChiefId = model.ChiefId,
            Name = model.Name,
            Salary = model.Salary,
            OldSalary = model.OldSalary
        };
    }

    public async Task<IEnumerable<UpdateSalaryResultDTO>> UpdateSalaryForDepartment(
        long departmentId, 
        int percent
    )
    {
        IEnumerable<UpdateSalaryForDepartment> result = await _departmentRepository.UpdateSalaryForDepartment(departmentId, percent);
        return result.Select(MapToUpdateSalaryResultDto);
    }

    public async Task<bool> IsExists(long id)
    {
        Department? department = await _departmentRepository.FindByIdAsync(id);
        return department != null;
    }
}