using NUnit.Framework;
using Moq;
using Bai2.Controllers;
using Bai2.Services;
using Bai2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Bai2.Tests
{
    [TestFixture]
    public class RookiesControllerTests : IDisposable
    {
        private Mock<IPersonService> _personServiceMock;
        private RookiesController _controller;

        [SetUp]
        public void Setup()
        {
            _personServiceMock = new Mock<IPersonService>();
            _controller = new RookiesController(_personServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Index_ReturnsViewResult_WithPaginationModel()
        {
            _personServiceMock.Setup(s => s.GetPagination(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PaginationModel<Person>());
            var result = _controller.Index();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void GetPeople_ReturnsOk_WithList()
        {
            _personServiceMock.Setup(s => s.GetAll()).Returns(new List<Person>());
            var result = _controller.GetPeople();
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetPerson_NotFound_WhenNull()
        {
            _personServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns((Person?)null);
            var result = _controller.GetPerson(1);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetPerson_Ok_WhenFound()
        {
            _personServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(new Person
            {
                Id = 1,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            });
            var result = _controller.GetPerson(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void CreatePerson_BadRequest_WhenModelInvalid()
        {
            _controller.ModelState.AddModelError("error", "error");
            var result = _controller.CreatePerson(new Person
            {
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "0123456789",
                IsGraduated = true
            });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void CreatePerson_CreatedAtAction_WhenValid()
        {
            var person = new Person
            {
                Id = 1,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
            _personServiceMock.Setup(s => s.Create(person));
            var result = _controller.CreatePerson(person);
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public void CreatePerson_CallsServiceCreate_WhenValid()
        {
            var person = new Person
            {
                Id = 2,
                PhoneNumber = "0123456789",
                FirstName = "Mock",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1995, 1, 1),
                IsGraduated = false
            };
            _personServiceMock.Setup(s => s.Create(person));
            _controller.CreatePerson(person);
            _personServiceMock.Verify(s => s.Create(It.Is<Person>(p => p.Id == 2 && p.FirstName == "Mock")), Times.Once);
        }

        [Test]
        public void UpdatePerson_BadRequest_WhenIdMismatch()
        {
            var person = new Person
            {
                Id = 2,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
            var result = _controller.UpdatePerson(1, person);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void UpdatePerson_NoContent_WhenValid()
        {
            var person = new Person
            {
                Id = 1,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            };
            _personServiceMock.Setup(s => s.Update(person));
            var result = _controller.UpdatePerson(1, person);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void DeletePerson_NotFound_WhenNull()
        {
            _personServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns((Person?)null);
            var result = _controller.DeletePerson(1);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void DeletePerson_NoContent_WhenFound()
        {
            _personServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(new Person
            {
                Id = 1,
                PhoneNumber = "0123456789",
                FirstName = "Test",
                LastName = "User",
                BirthPlace = "Test City",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsGraduated = true
            });
            _personServiceMock.Setup(s => s.Delete(It.IsAny<int>()));
            var result = _controller.DeletePerson(1);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Index_ReturnsViewResult_WithCorrectModel()
        {
            var expectedModel = new PaginationModel<Person>();
            _personServiceMock.Setup(s => s.GetPagination(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedModel);
            var result = _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetPeople_ReturnsOk_WithCorrectData()
        {
            var expectedPeople = new List<Person> { new Person {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            } };
            _personServiceMock.Setup(s => s.GetAll()).Returns(expectedPeople);
            var result = _controller.GetPeople() as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(expectedPeople));
        }

        [Test]
        public void GetPerson_ReturnsOk_WithCorrectPerson()
        {
            var expectedPerson = new Person
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            };
            _personServiceMock.Setup(s => s.GetById(1)).Returns(expectedPerson);
            var result = _controller.GetPerson(1) as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(expectedPerson));
        }

        [Test]
        public void CreatePerson_ReturnsBadRequest_WhenModelStateInvalid()
        {
            _controller.ModelState.AddModelError("FirstName", "Required");
            var result = _controller.CreatePerson(new Person
            {
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void CreatePerson_ReturnsCreatedAtAction_WithCorrectRouteValues()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            };
            _personServiceMock.Setup(s => s.Create(person));
            var result = _controller.CreatePerson(person) as CreatedAtActionResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(RookiesController.GetPerson)));
            Assert.That(result.RouteValues, Is.Not.Null);
            Assert.That(result.RouteValues["id"], Is.EqualTo(1));
        }

        [Test]
        public void UpdatePerson_ReturnsNoContent_WhenUpdateSuccessful()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            };
            _personServiceMock.Setup(s => s.Update(person));
            var result = _controller.UpdatePerson(1, person);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void DeletePerson_ReturnsNoContent_WhenDeleteSuccessful()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            };
            _personServiceMock.Setup(s => s.GetById(1)).Returns(person);
            _personServiceMock.Setup(s => s.Delete(1));
            var result = _controller.DeletePerson(1);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void GetPerson_ReturnsNotFound_WhenPersonNotFound()
        {
            _personServiceMock.Setup(s => s.GetById(999)).Returns((Person?)null);
            var result = _controller.GetPerson(999);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void UpdatePerson_ReturnsBadRequest_WhenIdMismatch()
        {
            var person = new Person
            {
                Id = 2,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0123456789",
                BirthPlace = "Test City"
            };
            var result = _controller.UpdatePerson(1, person);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void DeletePerson_ReturnsNotFound_WhenPersonNotFound()
        {
            _personServiceMock.Setup(s => s.GetById(999)).Returns((Person?)null);
            var result = _controller.DeletePerson(999);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
