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

    }
}
