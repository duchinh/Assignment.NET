using Microsoft.AspNetCore.Mvc;
using Assignment1.Services;
using Assignment1.Models;
using ClosedXML.Excel;


namespace Assignment1.Controllers
{
    [Route("NashTech/[controller]")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;

        public RookiesController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("GetMaleMembers")]
        public IActionResult GetMaleMembers()
        {
            var maleMembers = _personService.GetMaleMembers();
            return Ok(maleMembers);
        }

        [HttpGet("GetOldestMember")]
        public IActionResult GetOldestMember()
        {
            var oldestMember = _personService.GetOldestMember();
            return Ok(oldestMember);

        }

        [HttpGet("GetFullNames")]
        public IActionResult GetFullNames()
        {
            var fullNames = _personService.GetFullNames();
            return Ok(fullNames);
        }

        [HttpGet("GetMembersByBirthYear")]
        public IActionResult GetMembersByBirthYear([FromQuery] string action)
        {
            switch (action?.ToLower())
            {
                case "2000":
                    return Ok(_personService.GetMembersByBirthYear(2000));
                case "greater2000":
                    return Ok(_personService.GetMembersBornAfterYear(2000));
                case "less2000":
                    return Ok(_personService.GetMembersBornBeforeYear(2000));
                default:
                    return BadRequest("Invalid action parameter");
            }
        }

        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            var fileContent = _personService.ExportPeopleToExcel();

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "people.xlsx");
        }
    }
}