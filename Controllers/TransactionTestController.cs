using Microsoft.AspNetCore.Mvc;
using EFCoreWebAPI.Services;
using EFCoreWebAPI.Models;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("API để test transaction")]
    public class TransactionTestController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public TransactionTestController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Cập nhật lương cho nhân viên (test transaction thành công)
        /// </summary>
        /// <param name="employeeId">ID của nhân viên</param>
        /// <param name="newSalary">Lương mới (phải lớn hơn 0)</param>
        /// <returns>Thông tin về việc cập nhật lương</returns>
        [HttpPost("UpdateSalary/{employeeId}")]
        [SwaggerOperation(
            Summary = "Cập nhật lương cho nhân viên",
            Description = "API này sẽ cập nhật lương cho nhân viên và kiểm tra xem transaction có hoạt động đúng không"
        )]
        [SwaggerResponse(200, "Cập nhật lương thành công", typeof(object))]
        [SwaggerResponse(400, "Lương không hợp lệ")]
        [SwaggerResponse(404, "Không tìm thấy nhân viên")]
        [SwaggerResponse(500, "Lỗi server")]
        public async Task<IActionResult> TestUpdateSalary(
            [SwaggerParameter("ID của nhân viên")] int employeeId,
            [SwaggerParameter("Lương mới")] [FromBody] decimal newSalary)
        {
            try
            {
                // Kiểm tra lương hợp lệ
                if (newSalary <= 0)
                {
                    return BadRequest(new { message = "Lương phải lớn hơn 0" });
                }

                // Lấy thông tin nhân viên trước khi cập nhật
                var employeeBefore = await _employeeService.GetByIdAsync(employeeId);
                if (employeeBefore == null)
                {
                    return NotFound(new { message = $"Không tìm thấy nhân viên với ID {employeeId}" });
                }

                var oldSalary = employeeBefore.Salary?.Amount ?? 0;

                // Thực hiện cập nhật lương
                await _employeeService.UpdateEmployeeSalaryWithTransactionAsync(employeeId, newSalary);

                // Lấy thông tin nhân viên sau khi cập nhật
                var employeeAfter = await _employeeService.GetByIdAsync(employeeId);
                var updatedSalary = employeeAfter?.Salary?.Amount ?? 0;

                return Ok(new
                {
                    message = "Cập nhật lương thành công",
                    employeeId,
                    oldSalary,
                    newSalary,
                    updatedSalary,
                    isSuccess = newSalary == updatedSalary
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Đã xảy ra lỗi khi cập nhật lương", error = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật lương cho nhân viên (test transaction thất bại)
        /// </summary>
        /// <param name="employeeId">ID của nhân viên</param>
        /// <param name="newSalary">Lương mới (không sử dụng, sẽ cố tình gây lỗi)</param>
        /// <returns>Thông tin về việc rollback transaction</returns>
        [HttpPost("UpdateSalaryWithError/{employeeId}")]
        [SwaggerOperation(
            Summary = "Test transaction thất bại",
            Description = "API này sẽ cố tình gây lỗi bằng cách cập nhật lương âm để test rollback transaction"
        )]
        [SwaggerResponse(500, "Transaction thất bại và đã rollback", typeof(object))]
        [SwaggerResponse(404, "Không tìm thấy nhân viên")]
        public async Task<IActionResult> TestUpdateSalaryWithError(
            [SwaggerParameter("ID của nhân viên")] int employeeId,
            [SwaggerParameter("Lương mới (không sử dụng)")] [FromBody] decimal newSalary)
        {
            try
            {
                // Lấy thông tin nhân viên trước khi cập nhật
                var employeeBefore = await _employeeService.GetByIdAsync(employeeId);
                if (employeeBefore == null)
                {
                    return NotFound(new { message = $"Không tìm thấy nhân viên với ID {employeeId}" });
                }

                var oldSalary = employeeBefore.Salary?.Amount ?? 0;

                // Cố tình gây lỗi bằng cách cập nhật lương âm
                await _employeeService.UpdateEmployeeSalaryWithTransactionAsync(employeeId, -100);

                // Nếu không có lỗi, trả về kết quả
                return Ok(new
                {
                    message = "Cập nhật lương thành công",
                    employeeId,
                    oldSalary,
                    newSalary = -100
                });
            }
            catch (Exception ex)
            {
                // Lấy thông tin nhân viên sau khi rollback
                var employeeAfter = await _employeeService.GetByIdAsync(employeeId);
                var currentSalary = employeeAfter?.Salary?.Amount ?? 0;

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new
                    {
                        message = "Đã xảy ra lỗi khi cập nhật lương",
                        error = ex.Message,
                        employeeId,
                        currentSalary,
                        isRollbackSuccess = currentSalary > 0
                    });
            }
        }
    }
} 