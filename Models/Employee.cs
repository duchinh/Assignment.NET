using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public DateTime JoinedDate { get; set; }

        public int DepartmentId { get; set; }

        public required Department Department { get; set; }

        public Salary? Salary { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
