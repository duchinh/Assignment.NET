using Microsoft.AspNetCore.Mvc;
using Bai2.Models;
using Bai2.Services;

namespace Bai2.Controllers
{
    [Route("NashTech/[controller]/[action]")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;

        public RookiesController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var model = _personService.GetPagination(page, pageSize);

            return View(model);
        }

        [HttpGet]
        [Route("")]
        [Route("people")]
        public IActionResult GetPeople(int page = 1, int pageSize = 5)
        {
            var model = _personService.GetAll().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(model);
        }

        [HttpGet]
        [Route("people/{id:int}")]
        public IActionResult GetPerson(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("people")]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.Create(person);

                return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("people/{id:int}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _personService.Update(person);

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("people/{id:int}")]
        public IActionResult DeletePerson(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            _personService.Delete(id);

            return NoContent();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.Create(person);

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.Update(person);

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _personService.GetById(id);
            if (person != null)
            {
                _personService.Delete(id);
                TempData["Message"] = $"Person {person.FirstName} {person.LastName} was removed successfully!";

                return RedirectToAction(nameof(DeleteConfirmation));
            }

            return NotFound();
        }

        public IActionResult DeleteConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MaleMembers()
        {
            var males = _personService.GetAll()
                .Where(p => p.Gender == Gender.Male)
                .Select(p => $"{p.FirstName} {p.LastName}");

            return Content("Male Members: " + string.Join(", ", males));
        }

        [HttpGet]
        public IActionResult Oldest()
        {
            var oldest = _personService.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();

            return Content(
                _personService.GetAll()
                    .OrderBy(p => p.DateOfBirth)
                    .Select(p => $"Oldest Member: {p.FirstName} {p.LastName}, Date of Birth: {p.DateOfBirth.ToShortDateString()}")
                    .FirstOrDefault() ?? "No members found.");
        }

        [HttpGet]
        public IActionResult FullNames()
        {
            var fullNames = _personService.GetAll().Select(p => $"{p.LastName} {p.FirstName}");

            return Content("Full Names: " + string.Join(", ", fullNames));
        }

        [HttpGet]
        public IActionResult FilterByBirthYear(string filter)
        {
            switch (filter?.ToLower())
            {
                case "equal":
                    return RedirectToAction(nameof(BirthYear2000));
                case "greater":
                    return RedirectToAction(nameof(BirthYearGreaterThan2000));
                case "less":
                    return RedirectToAction(nameof(BirthYearLessThan2000));
                default:
                    return Content("Invalid filter. Please use 'equal', 'greater', or 'less'.");
            }
        }

        [HttpGet]
        public IActionResult BirthYear2000()
        {
            var a = _personService.GetAll().Where(p => p.DateOfBirth.Year == 2000).Select(p => $"{p.FirstName} {p.LastName}");

            return Content("Members born in 2000: " + string.Join(", ", a));
        }

        [HttpGet]
        public IActionResult BirthYearGreaterThan2000()
        {
            var b = _personService.GetAll().Where(p => p.DateOfBirth.Year > 2000).Select(p => $"{p.FirstName} {p.LastName}");

            return Content("Members born after 2000: " + string.Join(", ", b));
        }

        [HttpGet]
        public IActionResult BirthYearLessThan2000()
        {
            var c = _personService.GetAll().Where(p => p.DateOfBirth.Year < 2000).Select(p => $"{p.FirstName} {p.LastName}");

            return Content("Members born before 2000: " + string.Join(", ", c));
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            var excel = "First Name, Last Name, Gender, Date of Birth, Phone Number, Birth Place, Is Graduated\n" +
                string.Join("\r\n", _personService.GetAll().Select(p => $"{p.FirstName}, {p.LastName}, {p.Gender}, {p.DateOfBirth:yyyy-MM-dd}, {p.PhoneNumber}, {p.BirthPlace}, {p.IsGraduated}"));
            var bytes = System.Text.Encoding.UTF8.GetBytes(excel);

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xls");
        }
    }
}