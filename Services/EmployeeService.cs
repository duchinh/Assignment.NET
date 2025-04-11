using Microsoft.EntityFrameworkCore;
using EFCoreWebAPI.Data;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTOs;

namespace EFCoreWebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetEmployeesWithDepartmentAsync()
        {
            return await _context.Employees
                .Join(_context.Departments,
                    e => e.DepartmentId,
                    d => d.Id,
                    (e, d) => new
                    {
                        EmployeeId = e.Id,
                        EmployeeName = e.Name,
                        DepartmentName = d.Name
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetEmployeesWithProjectsAsync()
        {
            return await _context.Employees
                .GroupJoin(_context.ProjectEmployees,
                    e => e.Id,
                    pe => pe.EmployeeId,
                    (e, pe) => new
                    {
                        EmployeeId = e.Id,
                        EmployeeName = e.Name,
                        Projects = pe.Select(p => new
                        {
                            ProjectId = p.ProjectId,
                            ProjectName = p.Project.Name,
                            Enable = p.Enable
                        })
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetHighSalaryEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.Salary != null && e.Salary.Amount > 100)
                .Select(e => new
                {
                    EmployeeId = e.Id,
                    EmployeeName = e.Name,
                    Salary = e.Salary.Amount
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetHighSalaryEmployeesRawSQLAsync()
        {
            return await _context.Database
                .SqlQuery<object>($@"
                    SELECT e.Id as EmployeeId, e.Name as EmployeeName, s.Amount as Salary
                    FROM Employees e
                    INNER JOIN Salaries s ON e.Id = s.EmployeeId
                    WHERE s.Amount > 100")
                .ToListAsync();
        }

        Task<IEnumerable<EmployeeDTO>> IEmployeeService.GetEmployeesWithDepartmentAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<EmployeeDTO>> IEmployeeService.GetEmployeesWithProjectsAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<EmployeeDTO>> IEmployeeService.GetHighSalaryEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDTO>> GetEmployeesWithDepartmentInnerJoinAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDTO>> GetEmployeesWithProjectsLeftJoinAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDTO>> GetEmployeesWithHighSalaryAndRecentJoinAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployeesWithHighSalaryAndRecentJoinRawSQLAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployeeSalaryWithTransactionAsync(int employeeId, decimal newSalary)
        {
            throw new NotImplementedException();
        }
    }
}