namespace EFCoreWebAPI.Models
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool Enable { get; set; }
        public required Project Project { get; set; }
        public required Employee Employee { get; set; }
    }
}