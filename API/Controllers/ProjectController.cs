using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestProjectRepositoryMethods()
        {
            Console.WriteLine(DateTime.Now);
            var result = string.Empty;

            // Тестирование GetByIdAsync
            var projectById = await _projectRepository.GetByIdAsync(1);
            result += projectById is not null 
                ? $"Project by ID: {projectById.Name}" 
                : "Project not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allProjects = await _projectRepository.GetAllAsync();
            result += "<br>All projects:<br>";
            foreach (var project in allProjects)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newProject = new Project 
            { 
                Name = "New Project", 
                Status = "In Progress", 
                Profit = 100000m, 
                Cost = 50000m, 
                Deadline = DateTime.Now.AddMonths(6), 
                CustomerId = 3, 
                EditorId = 4 
            };
            await _projectRepository.AddAsync(newProject);
            result += "Added new project<br>";
            
            allProjects = await _projectRepository.GetAllAsync();
            result += "<br>All projects after adding:<br>";
            foreach (var project in allProjects)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var projectToUpdate = new Project 
            { 
                Id = 1, 
                Name = "Updated Project", 
                Status = "Completed", 
                Profit = 150000m, 
                Cost = 60000m, 
                Deadline = DateTime.Now.AddMonths(3), 
                CustomerId = 4, 
                EditorId = 3 
            };
            await _projectRepository.UpdateAsync(projectToUpdate);
            result += "Updated project<br>";
            
            allProjects = await _projectRepository.GetAllAsync();
            result += "<br>All projects after update:<br>";
            foreach (var project in allProjects)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _projectRepository.DeleteAsync(projectToUpdate);
            result += "Deleted project<br>";
            
            allProjects = await _projectRepository.GetAllAsync();
            result += "<br>All projects after deletion:<br>";
            foreach (var project in allProjects)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByCustomerIdAsync
            var projectsByCustomer = await _projectRepository.GetByCustomerIdAsync(1);
            result += "Projects by customer ID:<br>";
            foreach (var project in projectsByCustomer)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByEditorIdAsync
            var projectsByEditor = await _projectRepository.GetByEditorIdAsync(2);
            result += "Projects by editor ID:<br>";
            foreach (var project in projectsByEditor)
            {
                result += $"Project: {project.Name} (Status: {project.Status})<br>";
            }
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}
