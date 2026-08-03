using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Core.DTO.Department;

public class UpdateSalaryDTO
{
    [Required(ErrorMessage = "Отсутствует процент повышения зарплаты")]
    public int Percent {get; set;}
}