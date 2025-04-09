using System.ComponentModel.DataAnnotations;

namespace EFCoreWebAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    }
}
