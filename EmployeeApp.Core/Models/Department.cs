using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Core.Models;

[Table("department")]
public class Department
{
    [Key]
    [Column("id")]
    public long Id {get; set;}

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name {get; set;} = string.Empty;
}