namespace Assignment1.Models
{
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{LastName} {FirstName}";
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public bool IsGraduated { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other,
    }
}
