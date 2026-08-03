using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Core.Models;

public class UpdateSalaryForDepartment
{
    [Column("id")]
    public long Id {get; set;}

    [Column("department_id")]
    public long DepartmentId {get; set;}

    [Column("chief_id")]
    public long? ChiefId {get; set;}

    [Column("name")]
    public string Name {get; set;} = string.Empty;

    [Column("salary")]
    public int Salary {get; set;}

    [Column("old_salary")]
    public int OldSalary {get; set;}
}