using System.ComponentModel.DataAnnotations;

namespace EFCoreWebAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}