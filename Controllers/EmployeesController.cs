using Microsoft.AspNetCore.Mvc;
using EFCoreWebAPI.Services;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTOs;
using System.Net;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new { message = "Đã xảy ra lỗi khi lấy danh sách nhân viên", error = ex.Message });
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);

                if (employee == null)
                {
                    return NotFound(new { message = $"Không tìm thấy nhân viên với ID {id}" });
                }

                return employee;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên", error = ex.Message });
            }
        }

        // GET: api/Employees/WithDepartment
        [HttpGet("WithDepartment")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithDepartmentAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithDepartmentAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên với phòng ban", error = ex.Message });
            }
        }

        // GET: api/Employees/WithProjects
        [HttpGet("WithProjects")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithProjectsAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithProjectsAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên với dự án", error = ex.Message });
            }
        }

        // GET: api/Employees/HighSalary
        [HttpGet("HighSalary")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetHighSalaryEmployeesAsync()
        {
            try
            {
                var employees = await _employeeService.GetHighSalaryEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên có lương cao", error = ex.Message });
            }
        }

        // GET: api/Employees/WithDepartmentInnerJoin
        [HttpGet("WithDepartmentInnerJoin")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithDepartmentInnerJoinAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithDepartmentInnerJoinAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên với phòng ban (Inner Join)", error = ex.Message });
            }
        }

        // GET: api/Employees/WithProjectsLeftJoin
        [HttpGet("WithProjectsLeftJoin")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithProjectsLeftJoinAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithProjectsLeftJoinAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên với dự án (Left Join)", error = ex.Message });
            }
        }

        // GET: api/Employees/HighSalaryAndRecentJoin
        [HttpGet("HighSalaryAndRecentJoin")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithHighSalaryAndRecentJoinAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithHighSalaryAndRecentJoinAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên có lương cao và mới tham gia", error = ex.Message });
            }
        }

        // GET: api/Employees/HighSalaryAndRecentJoinRawSQL
        [HttpGet("HighSalaryAndRecentJoinRawSQL")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesWithHighSalaryAndRecentJoinRawSQLAsync()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithHighSalaryAndRecentJoinRawSQLAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi lấy thông tin nhân viên có lương cao và mới tham gia (Raw SQL)", error = ex.Message });
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest(new { message = "ID không khớp" });
                }

                await _employeeService.UpdateAsync(employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi cập nhật thông tin nhân viên", error = ex.Message });
            }
        }

        // PUT: api/Employees/UpdateSalary/5
        [HttpPut("UpdateSalary/{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeSalaryWithTransactionAsync(int employeeId, [FromBody] decimal newSalary)
        {
            try
            {
                await _employeeService.UpdateEmployeeSalaryWithTransactionAsync(employeeId, newSalary);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi cập nhật lương nhân viên", error = ex.Message });
            }
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                var createdEmployee = await _employeeService.CreateAsync(employee);
                return CreatedAtAction("GetEmployee", new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi thêm nhân viên mới", error = ex.Message });
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound(new { message = $"Không tìm thấy nhân viên với ID {id}" });
                }

                await _employeeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi xóa nhân viên", error = ex.Message });
            }
        }
    }
} 