namespace Assignment2.Domain.Entities
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public required string BirthPlace { get; set; }
    }
}