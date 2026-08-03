using EmployeeApp.Infrastructure.Data;
using EmployeeApp.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using EmployeeApp.Core.Models;

namespace EmployeeApp.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> FindByIdAsync(long id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<IEnumerable<UpdateSalaryForDepartment>> UpdateSalaryForDepartment(
        long departmentId, 
        int percent
    )
    {
        return await _context.UpdateDepartmentSalary
        .FromSqlInterpolated($"SELECT * FROM updatesalaryfordepartment({departmentId}, {percent})")
        .ToListAsync();
    }
}