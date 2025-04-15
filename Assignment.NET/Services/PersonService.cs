using Bai2.Models;
namespace Bai2.Services
{
    public class PersonService : IPersonService
    {
        private List<Person> _persons = new List<Person>{
            new Person {Id = 1, FirstName = "Nguyen", LastName = "Hue", Gender = Gender.Male, DateOfBirth = new DateTime(1990, 5, 1), PhoneNumber = "08742138911", BirthPlace = "Hung Yen", IsGraduated = true},
            new Person {Id = 2, FirstName = "Van", LastName = "Manh", Gender = Gender.Male, DateOfBirth = new DateTime(1999, 6, 2), PhoneNumber = "0986382411", BirthPlace = "Ha Tinh", IsGraduated = true},
            new Person {Id = 3, FirstName = "Nguyen", LastName = "Huyen", Gender = Gender.Female, DateOfBirth = new DateTime(2003, 7, 3), PhoneNumber = "0876892212", BirthPlace = "Nam Dinh", IsGraduated = true},
            new Person {Id = 4, FirstName = "Tran", LastName = "Anh", Gender = Gender.Female, DateOfBirth = new DateTime(2005, 8, 4), PhoneNumber = "0803902343", BirthPlace = "Ha Noi", IsGraduated = false},
            new Person {Id = 5, FirstName = "Vo", LastName = "Sau", Gender = Gender.Female, DateOfBirth = new DateTime(2006, 9, 5), PhoneNumber = "0932132123", BirthPlace = "Ninh Binh", IsGraduated = true},
            new Person {Id = 6, FirstName = "Le", LastName = "Minh", Gender = Gender.Male, DateOfBirth = new DateTime(1995, 3, 15), PhoneNumber = "0912345678", BirthPlace = "Hai Phong", IsGraduated = true},
            new Person {Id = 7, FirstName = "Pham", LastName = "Lan", Gender = Gender.Female, DateOfBirth = new DateTime(1998, 11, 20), PhoneNumber = "0923456789", BirthPlace = "Da Nang", IsGraduated = false},
            new Person {Id = 8, FirstName = "Hoang", LastName = "Khanh", Gender = Gender.Male, DateOfBirth = new DateTime(2001, 1, 10), PhoneNumber = "0934567890", BirthPlace = "Quang Ninh", IsGraduated = true},
            new Person {Id = 9, FirstName = "Tran", LastName = "Hoa", Gender = Gender.Female, DateOfBirth = new DateTime(1997, 7, 25), PhoneNumber = "0945678901", BirthPlace = "Hue", IsGraduated = true},
            new Person {Id = 10, FirstName = "Nguyen", LastName = "Tuan", Gender = Gender.Male, DateOfBirth = new DateTime(2000, 12, 5), PhoneNumber = "0956789012", BirthPlace = "Can Tho", IsGraduated = false}
        };

        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }

        public Person? GetById(int id)
        {
            return _persons.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Person person)
        {
            if (_persons.Any(p => p.Id == person.Id))
            {
                throw new InvalidOperationException("ID đã tồn tại");
            }
            _persons.Add(person);
        }

        public void Update(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson == null)
            {
                throw new InvalidOperationException("Không tìm thấy người dùng với id này");
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.Gender = person.Gender;
            existingPerson.DateOfBirth = person.DateOfBirth;
            existingPerson.BirthPlace = person.BirthPlace;
            existingPerson.IsGraduated = person.IsGraduated;
        }

        public void Delete(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                throw new InvalidOperationException("Không tìm thấy người dùng với ID này");
            }
            _persons.Remove(person);
        }


        public IEnumerable<Person> GetMaleMembers()
        {
            return _persons.Where(p => p.Gender == Gender.Male);
        }

        public Person GetOldestMember()
        {
            var oldest = _persons.OrderBy(p => p.DateOfBirth).FirstOrDefault();
            if (oldest == null)
            {
                throw new InvalidOperationException("No persons available to determine the oldest member.");
            }
            return oldest;
        }

        public string GetFullName(int id)
        {
            var person = GetById(id);
            return person != null ? $"{person.FirstName} {person.LastName}" : string.Empty;
        }

        public IEnumerable<Person> GetMembersByBirthYear(int year)
        {
            return _persons.Where(p => p.DateOfBirth.Year == year);
        }

        public IEnumerable<Person> GetMembersBornAfter2000()
        {
            return _persons.Where(p => p.DateOfBirth.Year > 2000);
        }

        public IEnumerable<Person> GetMembersBornBefore1990()
        {
            return _persons.Where(p => p.DateOfBirth.Year < 1990);
        }

        public IEnumerable<Person> GetGraduatedMembers()
        {
            return _persons.Where(p => p.IsGraduated);
        }

        public IEnumerable<Person> GetMembersByBirthPlace(string birthPlace)
        {
            return _persons.Where(p => p.BirthPlace.Equals(birthPlace, StringComparison.OrdinalIgnoreCase));
        }

        public PaginationModel<Person> GetPagination(int page, int pageSize)
        {
            var totalItems = _persons.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = _persons.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationModel<Person>
            {
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }
    }
}
