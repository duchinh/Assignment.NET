using System.ComponentModel.DataAnnotations;
namespace EFCoreWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int DepartmentId { get; set; }

        public required Department Department { get; set; }

        public required Salary Salary { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
