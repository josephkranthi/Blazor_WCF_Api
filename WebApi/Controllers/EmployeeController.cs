using WebApi.Models;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation("GET /api/Employee called");
            try
            {
                var employees = await _employeeService.GetEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployees");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            _logger.LogInformation("GET /api/Employee/{Id} called", id);
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    _logger.LogWarning("Employee {Id} not found", id);
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployee {Id}", id);
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogInformation("POST /api/Employee called");
            try
            {
                if (employee == null)
                {
                    _logger.LogWarning("CreateEmployee called with null employee");
                    return BadRequest(new { message = "Employee data is required" });
                }

                var id = await _employeeService.AddEmployeeAsync(employee);
                employee.Id = id;

                _logger.LogInformation("Employee created with Id {Id}", id);
                return CreatedAtAction(nameof(GetEmployee), new { id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateEmployee");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // PUT: api/Employee/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            _logger.LogInformation("PUT /api/Employee/{Id} called", id);
            try
            {
                if (employee == null)
                {
                    _logger.LogWarning("UpdateEmployee called with null employee");
                    return BadRequest(new { message = "Employee data is required" });
                }

                employee.Id = id;
                var success = await _employeeService.UpdateEmployeeAsync(employee);

                if (!success)
                {
                    _logger.LogWarning("Employee {Id} not found for update", id);
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                _logger.LogInformation("Employee {Id} updated", id);
                return Ok(new { message = $"Employee {id} updated successfully", employee });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateEmployee {Id}", id);
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation("DELETE /api/Employee/{Id} called", id);
            try
            {
                var success = await _employeeService.DeleteEmployeeAsync(id);

                if (!success)
                {
                    _logger.LogWarning("Employee {Id} not found for delete", id);
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                _logger.LogInformation("Employee {Id} deleted", id);
                return Ok(new { message = $"Employee {id} deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEmployee {Id}", id);
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
