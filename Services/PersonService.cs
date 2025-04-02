using Bai2.Models;
namespace Bai2.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _persons = new List<Person>{
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
            if (_persons.Any(p => p.PhoneNumber == person.PhoneNumber))
            {
                throw new InvalidOperationException("Số điện thoại đã tồn tại");
            }
            person.Id = _persons.Max(p => p.Id) + 1;
            _persons.Add(person);
        }

        public void Update(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson == null)
            {
                throw new InvalidOperationException("Không tìm thấy người dùng với ID này");
            }

            if (person.PhoneNumber != existingPerson.PhoneNumber && _persons.Any(p => p.PhoneNumber == person.PhoneNumber))
            {
                throw new InvalidOperationException("Số điện thoại đã tồn tại");
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.Gender = person.Gender;
            existingPerson.DateOfBirth = person.DateOfBirth;
            existingPerson.PhoneNumber = person.PhoneNumber;
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
            return _persons.OrderBy(p => p.DateOfBirth).First();
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

        public string GetExportExcelString()
        {
            return "First Name, Last Name, Gender, Date of Birth, Phone Number, Birth Place, Is Graduated\n" + 
                   string.Join("\r\n", _persons.Select(p => $"{p.FirstName}, {p.LastName}, {p.Gender}, {p.DateOfBirth:yyyy-MM-dd}, {p.PhoneNumber}, {p.BirthPlace}, {p.IsGraduated}"));
        }

        public string GetMaleMembersString()
        {
            var males = _persons.Where(p => p.Gender == Gender.Male)
                              .Select(p => $"{p.FirstName} {p.LastName}");
            return "Male Members: " + string.Join(", ", males);
        }

        public string GetOldestMemberString()
        {
            var oldest = _persons.OrderBy(p => p.DateOfBirth).FirstOrDefault();
            if (oldest != null)
            {
                return $"Oldest Member: {oldest.FirstName} {oldest.LastName}, Date of Birth: {oldest.DateOfBirth.ToShortDateString()}";
            }
            return "No members found.";
        }

        public string GetFullNamesString()
        {
            var fullNames = _persons.Select(p => $"{p.LastName} {p.FirstName}");
            return "Full Names: " + string.Join(", ", fullNames);
        }

        public string GetMembersByBirthYearString(string filter)
        {
            switch (filter?.ToLower())
            {
                case "equal":
                    var equal = _persons.Where(p => p.DateOfBirth.Year == 2000)
                                      .Select(p => $"{p.FirstName} {p.LastName}");
                    return "Members born in 2000: " + string.Join(", ", equal);
                case "greater":
                    var greater = _persons.Where(p => p.DateOfBirth.Year > 2000)
                                        .Select(p => $"{p.FirstName} {p.LastName}");
                    return "Members born after 2000: " + string.Join(", ", greater);
                case "less":
                    var less = _persons.Where(p => p.DateOfBirth.Year < 2000)
                                     .Select(p => $"{p.FirstName} {p.LastName}");
                    return "Members born before 2000: " + string.Join(", ", less);
                default:
                    return "Invalid filter. Please use 'equal', 'greater', or 'less'.";
            }
        }

        public Person GetPersonDetails(int id)
        {
            var person = GetById(id);
            if (person == null)
            {
                throw new InvalidOperationException("Không tìm thấy người dùng với ID này");
            }
            return person;
        }

        public Person GetPersonForEdit(int id)
        {
            return GetPersonDetails(id);
        }

        public Person GetPersonForDelete(int id)
        {
            return GetPersonDetails(id);
        }

        public object CreatePerson(Person person)
        {
            Create(person);
            return new { success = true };
        }

        public object UpdatePerson(Person person)
        {
            Update(person);
            return new { success = true };
        }

        public object DeletePerson(int id)
        {
            var person = GetById(id);
            if (person != null)
            {
                Delete(id);
                return new { success = true, message = $"Person {person.FirstName} {person.LastName} was removed from the list successfully!" };
            }
            return new { success = false, message = "Person not found" };
        }
    }
}
