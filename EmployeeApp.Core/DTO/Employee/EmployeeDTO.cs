namespace EmployeeApp.Core.DTO.Employee;

public class EmployeeDTO
{
    public long Id {get; set;}
    public long DepartmentId {get; set;}
    public long? ChiefId {get; set;}
    public string Name {get; set;} = string.Empty;
    public int Salary {get; set;}
}