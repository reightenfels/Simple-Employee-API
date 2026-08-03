using Microsoft.EntityFrameworkCore;
using EmployeeApp.Infrastructure.Data;
using EmployeeApp.Core.Models;
using EmployeeApp.Core.Contracts;

namespace EmployeeApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> FindAllAsync()
    {
        return await _context.Employees
        .Include(e => e.Chief)
        .Include(e => e.Department)
        .ToListAsync();
    }

    public async Task<Employee?> FindByIdAsync(long id)
    {
        return await _context.Employees
        .Include(e => e.Chief)
        .Include(e => e.Department)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Employee? employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}