using EmployeeApp.Core.Contracts;
using EmployeeApp.Core.DTO.Employee;
using EmployeeApp.Core.DTO.Requests;
using EmployeeApp.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Api.Controllers;

[ApiController]
[Route("api/v1/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public EmployeeController(
        IEmployeeService employeeService,
        IDepartmentService departmentService
    )
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    /// <summary>
    /// Получить список всех сотрудников
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
    {
        IEnumerable<EmployeeDTO?> employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    /// <summary>
    /// Получить данные сотрудника
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDTO>> GetEmployee(long id)
    {
        EmployeeDTO? employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    /// <summary>
    /// Создать или обновить сотрудника
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeDTO>> UpsertEmployee([FromBody] UpsertEmployeeDTO dto)
    {
        if (await _departmentService.IsExists(dto.DepartmentId) is false)
        {
            throw new ResourceNotFoundException($"Не найден отдел с ID: {dto.DepartmentId}");
        }

        if (dto.ChiefId != null && (await _employeeService.IsExists((long)dto.ChiefId) == false))
        {
            throw new ResourceNotFoundException($"Не найден шеф с ID: {dto.ChiefId}");
        }

        EmployeeDTO? employee = await _employeeService.UpsertAsync(dto);

        if (employee == null) return StatusCode(500, "Ошибка при создании/обновлении сотрудника");

        EmployeeDTO? upsertedEmployee = await _employeeService.GetByIdAsync(employee.Id);

        return Ok(upsertedEmployee);
    }

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee(long id)
    {
        if (await _employeeService.IsExists(id) is false)
        {
            throw new ResourceNotFoundException($"Не найден сотрудник с ID: {id}");
        }

        await _employeeService.DeleteAsync(id);
        return NoContent();
    }
}