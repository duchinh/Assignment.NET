using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTOs;

namespace EFCoreWebAPI.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);

        // Custom queries
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithDepartmentAsync();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithProjectsAsync();
        Task<IEnumerable<EmployeeDTO>> GetHighSalaryEmployeesAsync();
        Task<IEnumerable<object>> GetHighSalaryEmployeesRawSQLAsync();

        // New methods
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithDepartmentInnerJoinAsync();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithProjectsLeftJoinAsync();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithHighSalaryAndRecentJoinAsync();
        Task<IEnumerable<Employee>> GetEmployeesWithHighSalaryAndRecentJoinRawSQLAsync();
        Task UpdateEmployeeSalaryWithTransactionAsync(int employeeId, decimal newSalary);
    }
} 