using Assignment1.Models;

namespace Assignment1.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _people = new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Gender = Gender.Male, DateOfBirth = new DateTime(1990, 1, 1), PhoneNumber = "1234567890", BirthPlace = "New York", IsGraduated = true },
                new Person { FirstName = "Jane", LastName = "Smith", Gender = Gender.Female, DateOfBirth = new DateTime(2000, 5, 15), PhoneNumber = "0987654321", BirthPlace = "Los Angeles", IsGraduated = false },
                new Person { FirstName = "Mike", LastName = "Johnson", Gender = Gender.Male, DateOfBirth = new DateTime(1995, 8, 20), PhoneNumber = "5555555555", BirthPlace = "Chicago", IsGraduated = true },
                new Person { FirstName = "Sarah", LastName = "Williams", Gender = Gender.Female, DateOfBirth = new DateTime(2002, 3, 10), PhoneNumber = "1112223333", BirthPlace = "Houston", IsGraduated = false },
                new Person { FirstName = "Alice", LastName = "Brown", Gender = Gender.Female, DateOfBirth = new DateTime(1988, 12, 5), PhoneNumber = "2223334444", BirthPlace = "Seattle", IsGraduated = true },
                new Person { FirstName = "Bob", LastName = "Martin", Gender = Gender.Male, DateOfBirth = new DateTime(1992, 7, 22), PhoneNumber = "7778889999", BirthPlace = "Miami", IsGraduated = false },
                new Person { FirstName = "Emma", LastName = "Davis", Gender = Gender.Female, DateOfBirth = new DateTime(1993, 4, 12), PhoneNumber = "3334445555", BirthPlace = "Boston", IsGraduated = true },
                new Person { FirstName = "Liam", LastName = "Garcia", Gender = Gender.Male, DateOfBirth = new DateTime(1997, 6, 23), PhoneNumber = "4445556666", BirthPlace = "San Francisco", IsGraduated = false },
                new Person { FirstName = "Olivia", LastName = "Wilson", Gender = Gender.Female, DateOfBirth = new DateTime(1995, 11, 30), PhoneNumber = "5556667777", BirthPlace = "Atlanta", IsGraduated = true },
                new Person { FirstName = "Noah", LastName = "Martinez", Gender = Gender.Male, DateOfBirth = new DateTime(1989, 9, 17), PhoneNumber = "6667778888", BirthPlace = "Denver", IsGraduated = false },
                new Person { FirstName = "Ava", LastName = "Rodriguez", Gender = Gender.Female, DateOfBirth = new DateTime(2001, 2, 14), PhoneNumber = "7778889999", BirthPlace = "Philadelphia", IsGraduated = true },
                new Person { FirstName = "Ethan", LastName = "Hernandez", Gender = Gender.Male, DateOfBirth = new DateTime(1998, 8, 5), PhoneNumber = "8889990000", BirthPlace = "Phoenix", IsGraduated = false },
                new Person { FirstName = "Sophia", LastName = "Lopez", Gender = Gender.Female, DateOfBirth = new DateTime(2003, 7, 21), PhoneNumber = "9990001111", BirthPlace = "Dallas", IsGraduated = true },
                new Person { FirstName = "Mason", LastName = "Gonzalez", Gender = Gender.Male, DateOfBirth = new DateTime(1994, 10, 3), PhoneNumber = "0001112222", BirthPlace = "Austin", IsGraduated = false },
                new Person { FirstName = "Isabella", LastName = "Perez", Gender = Gender.Female, DateOfBirth = new DateTime(1999, 12, 17), PhoneNumber = "1112223333", BirthPlace = "San Diego", IsGraduated = true },
                new Person { FirstName = "Logan", LastName = "Sanchez", Gender = Gender.Male, DateOfBirth = new DateTime(1996, 5, 9), PhoneNumber = "2223334444", BirthPlace = "Las Vegas", IsGraduated = false }
            };

        public List<Person> GetAll()
        {
            return _people;
        }

        public List<Person> GetMaleMembers()
        {
            return _people.Where(p => p.Gender == Gender.Male).ToList();
        }

        public Person GetOldestMember()
        {
            return _people.OrderBy(p => p.DateOfBirth).First();
        }

        public List<string> GetFullNames()
        {
            return _people.Select(p => $"{p.LastName} {p.FirstName}").ToList();
        }

        public List<Person> GetMembersByBirthYear(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year == year).ToList();
        }

        public List<Person> GetMembersBornAfterYear(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year > year).ToList();
        }

        public List<Person> GetMembersBornBeforeYear(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year < year).ToList();
        }
    }
}
