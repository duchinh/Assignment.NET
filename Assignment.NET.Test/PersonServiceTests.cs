using Bai2.Services;
using Bai2.Models;
using System;
using System.Linq;

namespace Bai2.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private PersonService _service;

        [SetUp]
        public void Setup()
        {
            _service = new PersonService();
        }

        [Test]
        public void GetAll_ReturnsList()
        {
            var result = _service.GetAll();
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void GetById_ReturnsPerson()
        {
            var person = _service.GetById(1);
            Assert.That(person, Is.Not.Null);
            Assert.That(person.PhoneNumber, Is.EqualTo("08742138911"));
        }

        [Test]
        public void Create_Throws_WhenDuplicateId()
        {
            var person = _service.GetById(1);
            Assert.That(person, Is.Not.Null);
            person!.PhoneNumber = "08742138911";
            Assert.Throws<InvalidOperationException>(() => _service.Create(person));
        }

        [Test]
        public void Update_Throws_WhenNotFound()
        {
            var person = new Person
            {
                Id = 999,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
            Assert.Throws<InvalidOperationException>(() => _service.Update(person));
        }

        [Test]
        public void Delete_Throws_WhenNotFound()
        {
            Assert.Throws<InvalidOperationException>(() => _service.Delete(999));
        }

        [Test]
        public void GetMaleMembers_ReturnsOnlyMales()
        {
            var result = _service.GetMaleMembers();
            Assert.That(result.All(p => p.Gender == Gender.Male), Is.True);
        }

        [Test]
        public void GetOldestMember_ReturnsPerson()
        {
            var result = _service.GetOldestMember();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetFullName_ReturnsString()
        {
            var result = _service.GetFullName(1);
            Assert.That(result, Is.EqualTo("Nguyen Hue"));
        }

        [Test]
        public void GetMembersByBirthYear_ReturnsCorrect()
        {
            var result = _service.GetMembersByBirthYear(2000);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetMembersBornAfter2000_ReturnsCorrect()
        {
            var result = _service.GetMembersBornAfter2000();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetGraduatedMembers_ReturnsGraduated()
        {
            var result = _service.GetGraduatedMembers();
            Assert.That(result.All(p => p.IsGraduated), Is.True);
        }

        [Test]
        public void GetMembersByBirthPlace_ReturnsCorrect()
        {
            var result = _service.GetMembersByBirthPlace("Ha Noi");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetPagination_ReturnsPaginationModel()
        {
            var result = _service.GetPagination(1, 5);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Items.Count, Is.LessThanOrEqualTo(5));
        }

        [Test]
        public void Create_ValidPerson_AddsToCollection()
        {
            var person = new Person
            {
                Id = 999,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
            _service.Create(person);
            var result = _service.GetById(999);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo("Test"));
        }

        [Test]
        public void Update_ValidPerson_UpdatesSuccessfully()
        {
            var person = new Person
            {
                Id = 1,
                PhoneNumber = "08742138911",
                FirstName = "Updated",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = false
            };
            _service.Update(person);
            var updated = _service.GetById(1);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.FirstName, Is.EqualTo("Updated"));
            Assert.That(updated.IsGraduated, Is.False);
        }

        [Test]
        public void Delete_ValidId_RemovesPerson()
        {
            _service.Delete(1);
            var person = _service.GetById(1);
            Assert.That(person, Is.Null);
        }

        [Test]
        public void GetMembersByBirthYear_EmptyResult_WhenNoMatches()
        {
            var result = _service.GetMembersByBirthYear(9999);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetMembersByBirthPlace_EmptyResult_WhenNoMatches()
        {
            var result = _service.GetMembersByBirthPlace("NonExistentPlace");
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetPagination_ReturnsEmpty_WhenPageOutOfRange()
        {
            var result = _service.GetPagination(999, 5);
            Assert.That(result.Items, Is.Empty);
        }

        [Test]
        public void GetPagination_ReturnsCorrectPageSize()
        {
            var result = _service.GetPagination(1, 2);
            Assert.That(result.Items.Count, Is.LessThanOrEqualTo(2));
        }

        [Test]
        public void GetFullName_ReturnsEmpty_WhenPersonNotFound()
        {
            var result = _service.GetFullName(999); // id không tồn tại
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetOldestMember_ReturnsNull_WhenNoMembers()
        {
            var fake = new FakePersonService();
            var result = fake.GetOldestMember();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetGraduatedMembers_ReturnsEmpty_WhenNoGraduated()
        {
            var fake = new FakePersonService();
            var result = fake.GetGraduatedMembers();
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetMembersBornAfter2000_ReturnsEmpty_WhenNoMatches()
        {
            var fake = new FakePersonService();
            var result = fake.GetMembersBornAfter2000();
            Assert.That(result, Is.Empty);
        }

        // Stub Example: IPersonService trả về dữ liệu cố định
        private class PersonServiceStub : IPersonService
        {
            public IEnumerable<Person> GetAll() => new List<Person> { new Person { Id = 1, FirstName = "Stub", LastName = "User", PhoneNumber = "0000000000", BirthPlace = "StubCity" } };
            public Person? GetById(int id) => new Person { Id = id, FirstName = "Stub", LastName = "User", PhoneNumber = "0000000000", BirthPlace = "StubCity" };
            public void Create(Person person) { }
            public void Update(Person person) { }
            public void Delete(int id) { }
            public List<Person> GetMaleMembers() => new List<Person>();
            public Person? GetOldestMember() => null;
            public string GetFullName(int id) => "Stub User";
            public List<Person> GetMembersByBirthYear(int year) => new List<Person>();
            public List<Person> GetMembersBornAfter2000() => new List<Person>();
            public List<Person> GetGraduatedMembers() => new List<Person>();
            public List<Person> GetMembersByBirthPlace(string place) => new List<Person>();
            public PaginationModel<Person> GetPagination(int page, int pageSize) => new PaginationModel<Person>();
        }

        [Test]
        public void Stub_GetAll_ReturnsStubData()
        {
            var stub = new PersonServiceStub();
            var result = stub.GetAll();
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().FirstName, Is.EqualTo("Stub"));
            Assert.That(result.First().PhoneNumber, Is.EqualTo("0000000000"));
            Assert.That(result.First().BirthPlace, Is.EqualTo("StubCity"));
        }

        // Fake Example: FakePersonService lưu trữ dữ liệu trong memory
        private class FakePersonService : IPersonService
        {
            private readonly List<Person> _people = new();
            public IEnumerable<Person> GetAll() => _people;
            public Person? GetById(int id) => _people.FirstOrDefault(p => p.Id == id);
            public void Create(Person person) => _people.Add(person);
            public void Update(Person person)
            {
                var idx = _people.FindIndex(p => p.Id == person.Id);
                if (idx >= 0) _people[idx] = person;
            }
            public void Delete(int id) => _people.RemoveAll(p => p.Id == id);
            public List<Person> GetMaleMembers() => _people.Where(p => p.Gender == Gender.Male).ToList();
            public Person? GetOldestMember() => _people.OrderBy(p => p.DateOfBirth).FirstOrDefault();
            public string GetFullName(int id) => _people.FirstOrDefault(p => p.Id == id) is { } p ? $"{p.FirstName} {p.LastName}" : string.Empty;
            public List<Person> GetMembersByBirthYear(int year) => _people.Where(p => p.DateOfBirth.Year == year).ToList();
            public List<Person> GetMembersBornAfter2000() => _people.Where(p => p.DateOfBirth.Year > 2000).ToList();
            public List<Person> GetGraduatedMembers() => _people.Where(p => p.IsGraduated).ToList();
            public List<Person> GetMembersByBirthPlace(string place) => _people.Where(p => p.BirthPlace == place).ToList();
            public PaginationModel<Person> GetPagination(int page, int pageSize)
            {
                var items = _people.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return new PaginationModel<Person> { Items = items };
            }
        }

        [Test]
        public void Fake_CreateAndGetAll_WorksCorrectly()
        {
            var fake = new FakePersonService();
            fake.Create(new Person { Id = 10, FirstName = "Fake", LastName = "User", Gender = Gender.Male, DateOfBirth = new DateTime(2001, 1, 1), PhoneNumber = "1111111111", BirthPlace = "FakeCity" });
            var all = fake.GetAll();
            Assert.That(all.Count(), Is.EqualTo(1));
            Assert.That(all.First().FirstName, Is.EqualTo("Fake"));
            Assert.That(all.First().PhoneNumber, Is.EqualTo("1111111111"));
            Assert.That(all.First().BirthPlace, Is.EqualTo("FakeCity"));
        }
    }
}
