namespace EmployeeApp.Core.DTO.Employee;

public class EmployeeDTO
{
    public long Id {get; set;}
    public string DepartmentName {get; set;} = string.Empty;
    public string? ChiefName {get; set;}
    public string Name {get; set;} = string.Empty;
    public int Salary {get; set;}
}