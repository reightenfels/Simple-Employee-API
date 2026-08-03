using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Core.Models;

[Table("employee")]
public class Employee
{
    [Key]
    [Column("id")]
    public long Id {get; set;}

    [Column("department_id")]
    public long DepartmentId {get;set;}

    [ForeignKey(nameof(DepartmentId))]
    [Required]
    public Department Department {get; set;} = null!;

    [Column("chief_id")]
    public long? ChiefId {get; set;}

    [ForeignKey(nameof(ChiefId))]
    public Employee? Chief {get; set;}

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name {get; set;} = string.Empty;

    [Required]
    [Column("salary")]
    public int Salary {get; set;}
}