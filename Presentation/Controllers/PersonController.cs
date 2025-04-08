using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople() 
        {
            var people = await _personService.GetPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        [HttpGet("filter/name/{name}")]
        public async Task<IActionResult> FilterByName(string name) 
        {
            var people = await _personService.FilterByName(name);
            return Ok(people);
        }

        [HttpGet("filter/gender/{gender}")]
        public async Task<IActionResult> FilterByGender(string gender) 
        {
            var people = await _personService.FilterByGender(gender);
            return Ok(people);
        }

        [HttpGet("filter/birthplace/{birthPlace}")]
        public async Task<IActionResult> FilterByBirthPlace(string birthPlace) 
        {
            var people = await _personService.FilterByBirthPlace(birthPlace);
            return Ok(people);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            await _personService.AddPerson(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person person)
        {
            var existingPerson = await _personService.GetPersonById(id);
            if (existingPerson == null)
                return NotFound();

            await _personService.UpdatePerson(id, person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var existingPerson = await _personService.GetPersonById(id);
            if (existingPerson == null)
                return NotFound();

            await _personService.DeletePerson(id);
            return NoContent();
        }
    }
} 