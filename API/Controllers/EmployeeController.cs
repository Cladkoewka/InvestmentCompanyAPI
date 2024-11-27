using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestEmployeeRepositoryMethods()
        {
            var result = string.Empty;

            // Тестирование GetByIdAsync
            var employeeById = await _employeeRepository.GetByIdAsync(1);
            result += employeeById is not null 
                ? $"Employee by ID: {employeeById.FirstName} {employeeById.LastName}" 
                : "Employee not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allEmployees = await _employeeRepository.GetAllAsync();
            result += "<br>All employees:<br>";
            foreach (var employee in allEmployees)
            {
                result += $"Employee: {employee.FirstName} {employee.LastName}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newEmployee = new Employee { FirstName = "Alice", LastName = "Smith", DepartmentId = 3 };
            await _employeeRepository.AddAsync(newEmployee);
            result += "Added new employee<br>";
            
            allEmployees = await _employeeRepository.GetAllAsync();
            result += "<br>All employees after adding:<br>";
            foreach (var employee in allEmployees)
            {
                result += $"Employee: {employee.FirstName} {employee.LastName}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var employeeToUpdate = new Employee { Id = 2, FirstName = "Alice", LastName = "Johnson", DepartmentId = 4 };
            await _employeeRepository.UpdateAsync(employeeToUpdate);
            result += "Updated employee<br>";
            
            allEmployees = await _employeeRepository.GetAllAsync();
            result += "<br>All employees after update:<br>";
            foreach (var employee in allEmployees)
            {
                result += $"Employee: {employee.FirstName} {employee.LastName}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _employeeRepository.DeleteAsync(employeeToUpdate);
            result += "Deleted employee<br>";
            
            allEmployees = await _employeeRepository.GetAllAsync();
            result += "<br>All employees after deletion:<br>";
            foreach (var employee in allEmployees)
            {
                result += $"Employee: {employee.FirstName} {employee.LastName}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByDepartmentIdAsync
            var employeesByDepartment = await _employeeRepository.GetByDepartmentIdAsync(1);
            result += "Employees in department 1:<br>";
            foreach (var employee in employeesByDepartment)
            {
                result += $"Employee: {employee.FirstName} {employee.LastName}<br>";
            }
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}
