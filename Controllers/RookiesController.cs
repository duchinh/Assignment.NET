using Microsoft.AspNetCore.Mvc;
using Bai2.Models;
using Bai2.Services;

namespace Bai2.Controllers
{
    [Route("NashTech/[controller]/[action]")]
    [Route("api/[controller]")]
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
            return View(_personService.GetPagination(page, pageSize));
        }

        [HttpGet]
        [Route("")]
        [Route("people")]
        public IActionResult GetPeople(int page = 1, int pageSize = 5)
        {
            var model = _personService.GetPagination(page, pageSize);
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
            return View(_personService.GetPersonDetails(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            return RedirectToAction(nameof(Index), _personService.CreatePerson(person));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_personService.GetPersonForEdit(id));
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            return RedirectToAction(nameof(Index), _personService.UpdatePerson(person));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_personService.GetPersonForDelete(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(DeleteConfirmation), _personService.DeletePerson(id));
        }

        public IActionResult DeleteConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MaleMembers()
        {
            return Content(_personService.GetMaleMembersString());
        }

        [HttpGet]
        public IActionResult Oldest()
        {
            return Content(_personService.GetOldestMemberString());
        }

        [HttpGet]
        public IActionResult FullNames()
        {
            return Content(_personService.GetFullNamesString());
        }

        [HttpGet]
        public IActionResult FilterByBirthYear(string filter)
        {
            return Content(_personService.GetMembersByBirthYearString(filter));
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            var excel = _personService.GetExportExcelString();
            var bytes = System.Text.Encoding.UTF8.GetBytes(excel);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xls");
        }
    }
}