using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreWebAPI.Data;
using EFCoreWebAPI.Models;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectEmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectEmployee>>> GetProjectEmployees()
        {
            return await _context.ProjectEmployees.ToListAsync();
        }

        // GET: api/ProjectEmployees/5/1
        [HttpGet("{projectId}/{employeeId}")]
        public async Task<ActionResult<ProjectEmployee>> GetProjectEmployee(int projectId, int employeeId)
        {
            var projectEmployee = await _context.ProjectEmployees
                .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);

            if (projectEmployee == null)
            {
                return NotFound();
            }

            return projectEmployee;
        }

        // PUT: api/ProjectEmployees/5/1
        [HttpPut("{projectId}/{employeeId}")]
        public async Task<IActionResult> PutProjectEmployee(int projectId, int employeeId, ProjectEmployee projectEmployee)
        {
            if (projectId != projectEmployee.ProjectId || employeeId != projectEmployee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(projectEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectEmployeeExists(projectId, employeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectEmployees
        [HttpPost]
        public async Task<ActionResult<ProjectEmployee>> PostProjectEmployee(ProjectEmployee projectEmployee)
        {
            _context.ProjectEmployees.Add(projectEmployee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectEmployeeExists(projectEmployee.ProjectId, projectEmployee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjectEmployee", 
                new { projectId = projectEmployee.ProjectId, employeeId = projectEmployee.EmployeeId }, 
                projectEmployee);
        }

        // DELETE: api/ProjectEmployees/5/1
        [HttpDelete("{projectId}/{employeeId}")]
        public async Task<IActionResult> DeleteProjectEmployee(int projectId, int employeeId)
        {
            var projectEmployee = await _context.ProjectEmployees
                .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
            if (projectEmployee == null)
            {
                return NotFound();
            }

            _context.ProjectEmployees.Remove(projectEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectEmployeeExists(int projectId, int employeeId)
        {
            return _context.ProjectEmployees.Any(e => e.ProjectId == projectId && e.EmployeeId == employeeId);
        }
    }
} 