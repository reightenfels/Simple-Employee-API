using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Core.DTO.Requests;

public class UpsertEmployeeDTO
{
    public long? Id {get; set;}
    
    [Required(ErrorMessage = "Отсутствует отдел сотрудника")]
    public long DepartmentId {get; set;}

    public long? ChiefId {get; set;}

    [Required(ErrorMessage = "Отсутствует имя сотрудника")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя сотрудника от 2 до 100 символов")]
    public string Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "Отсутствует зарплата сотрудника")]
    public int Salary {get; set;}
}