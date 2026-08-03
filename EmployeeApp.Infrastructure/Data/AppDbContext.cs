using Microsoft.EntityFrameworkCore;
using EmployeeApp.Core.Models;

namespace EmployeeApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments {get; set;}
    public DbSet<UpdateSalaryForDepartment> UpdateDepartmentSalary {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UpdateSalaryForDepartment>()
            .HasNoKey()
            .ToView(null);
    }
}