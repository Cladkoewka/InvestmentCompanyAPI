using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("by-department/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = await _employeeService.GetByDepartmentIdAsync(departmentId);
            return Ok(employees);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var employee = await _employeeService.AddAsync(dto);
            return StatusCode(201, employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _employeeService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
