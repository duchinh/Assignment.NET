namespace EFCoreWebAPI.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public decimal? SalaryAmount { get; set; }
        public List<string> ProjectNames { get; set; } = new List<string>();
    }
} 