using Microsoft.AspNetCore.Mvc;
using EFCoreWebAPI.Services;
using EFCoreWebAPI.Models;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            try
            {
                await _departmentService.UpdateAsync(department);
            }
            catch
            {
                if (await _departmentService.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var createdDepartment = await _departmentService.CreateAsync(department);
            return CreatedAtAction("GetDepartment", new { id = createdDepartment.Id }, createdDepartment);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
    }
} 