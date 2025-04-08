using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.People.Any())
            {
                var people = new List<Person>
                {
                    new Person
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = Gender.Male,
                        BirthPlace = "New York"
                    },
                    new Person
                    {
                        FirstName = "Jane",
                        LastName = "Smith",
                        DateOfBirth = new DateTime(1992, 5, 15),
                        Gender = Gender.Female,
                        BirthPlace = "Los Angeles"
                    },
                    new Person
                    {
                        FirstName = "Michael",
                        LastName = "Johnson",
                        DateOfBirth = new DateTime(1988, 8, 20),
                        Gender = Gender.Male,
                        BirthPlace = "Chicago"
                    },
                    new Person
                    {
                        FirstName = "Emily",
                        LastName = "Williams",
                        DateOfBirth = new DateTime(1995, 3, 10),
                        Gender = Gender.Female,
                        BirthPlace = "Houston"
                    },
                    new Person
                    {
                        FirstName = "David",
                        LastName = "Brown",
                        DateOfBirth = new DateTime(1993, 11, 25),
                        Gender = Gender.Male,
                        BirthPlace = "Phoenix"
                    },
                    new Person
                    {
                        FirstName = "Sarah",
                        LastName = "Davis",
                        DateOfBirth = new DateTime(1991, 7, 5),
                        Gender = Gender.Female,
                        BirthPlace = "Philadelphia"
                    },
                    new Person
                    {
                        FirstName = "Robert",
                        LastName = "Miller",
                        DateOfBirth = new DateTime(1989, 4, 30),
                        Gender = Gender.Male,
                        BirthPlace = "San Antonio"
                    },
                    new Person
                    {
                        FirstName = "Jennifer",
                        LastName = "Wilson",
                        DateOfBirth = new DateTime(1994, 9, 12),
                        Gender = Gender.Female,
                        BirthPlace = "San Diego"
                    },
                    new Person
                    {
                        FirstName = "William",
                        LastName = "Moore",
                        DateOfBirth = new DateTime(1992, 2, 18),
                        Gender = Gender.Male,
                        BirthPlace = "Dallas"
                    },
                    new Person
                    {
                        FirstName = "Jessica",
                        LastName = "Taylor",
                        DateOfBirth = new DateTime(1990, 6, 8),
                        Gender = Gender.Female,
                        BirthPlace = "San Jose"
                    }
                };

                context.People.AddRange(people);
                context.SaveChanges();
            }
        }
    }
} 