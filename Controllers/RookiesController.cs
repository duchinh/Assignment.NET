using Microsoft.AspNetCore.Mvc;
using Assignment1.Services;
using Assignment1.Models;
using ClosedXML.Excel;
using System.IO;

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
            var people = _personService.GetAll();
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("People");

            // Add headers
            worksheet.Cell(1, 1).Value = "First Name";
            worksheet.Cell(1, 2).Value = "Last Name";
            worksheet.Cell(1, 3).Value = "Gender";
            worksheet.Cell(1, 4).Value = "Date of Birth";
            worksheet.Cell(1, 5).Value = "Phone Number";
            worksheet.Cell(1, 6).Value = "Birth Place";
            worksheet.Cell(1, 7).Value = "Is Graduated";

            // Add data
            int row = 2;
            foreach (var person in people)
            {
                worksheet.Cell(row, 1).Value = person.FirstName;
                worksheet.Cell(row, 2).Value = person.LastName;
                worksheet.Cell(row, 3).Value = person.FullName;
                worksheet.Cell(row, 4).Value = person.DateOfBirth;
                worksheet.Cell(row, 5).Value = person.PhoneNumber;
                worksheet.Cell(row, 6).Value = person.BirthPlace;
                worksheet.Cell(row, 7).Value = person.IsGraduated;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "people.xlsx");
        }
    }
}