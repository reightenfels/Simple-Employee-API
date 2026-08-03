using EmployeeApp.Core.DTO.Department;
using EmployeeApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Api.Controllers;

[ApiController]
[Route("api/v1/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    /// <summary>
    /// Повысить зарплату всем сотрудникам отдела
    /// </summary>
    [HttpPost("{id}/salary")]
    [ProducesResponseType(typeof(IEnumerable<UpdateSalaryResultDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UpdateSalaryResultDTO>>> UpdateDepartmentSalary(long id, [FromBody] UpdateSalaryDTO dto)
    {
        IEnumerable<UpdateSalaryResultDTO> result = await _departmentService.UpdateSalaryForDepartment(id, dto.Percent);
        return Ok(result);
    }
}