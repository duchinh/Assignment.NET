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
            Assert.That(person.PhoneNumber, Is.Not.Null);
        }

        [Test]
        public void Create_Throws_WhenDuplicateId()
        {
            var person = _service.GetById(1);
            person.PhoneNumber = "0123456789";
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
            Assert.That(result, Is.Not.Null);
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
    }
}
