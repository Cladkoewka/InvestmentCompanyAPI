using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestDepartmentRepositoryMethods()
        {
            var result = string.Empty;

            // Тестирование GetByIdAsync
            var departmentById = await _departmentRepository.GetByIdAsync(1);
            result += departmentById is not null 
                ? $"Department by ID: {departmentById.Name}" 
                : "Department not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allDepartments = await _departmentRepository.GetAllAsync();
            result += "<br>All departments:<br>";
            foreach (var department in allDepartments)
            {
                result += $"Department: {department.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newDepartment = new Department { Name = "New Department" };
            await _departmentRepository.AddAsync(newDepartment);
            result += "Added new department<br>";
            
            allDepartments = await _departmentRepository.GetAllAsync();
            result += "<br>All departments after adding:<br>";
            foreach (var department in allDepartments)
            {
                result += $"Department: {department.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var departmentToUpdate = new Department { Id = 1, Name = "Updated Department" };
            await _departmentRepository.UpdateAsync(departmentToUpdate);
            result += "Updated department<br>";
            
            allDepartments = await _departmentRepository.GetAllAsync();
            result += "<br>All departments after update:<br>";
            foreach (var department in allDepartments)
            {
                result += $"Department: {department.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _departmentRepository.DeleteAsync(departmentToUpdate);
            result += "Deleted department<br>";
            
            allDepartments = await _departmentRepository.GetAllAsync();
            result += "<br>All departments after deletion:<br>";
            foreach (var department in allDepartments)
            {
                result += $"Department: {department.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByNameAsync
            var departmentByName = await _departmentRepository.GetByNameAsync("New Department");
            result += "Found department by name:<br>";
            if (departmentByName != null)
            {
                result += $"Found department by name: {departmentByName.Name}<br>";
            }
            else
            {
                result += "Department not found by name.<br>";
            }

            return Content(result, "text/html");
        }
    }
}
