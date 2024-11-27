using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LinkControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectDepartmentLinkController : ControllerBase
    {
        private readonly IProjectDepartmentLinkRepository _projectDepartmentLinkRepository;

        public ProjectDepartmentLinkController(IProjectDepartmentLinkRepository projectDepartmentLinkRepository)
        {
            _projectDepartmentLinkRepository = projectDepartmentLinkRepository;
        }

        // Метод для тестирования методов репозитория
        [HttpGet("test")]
        public async Task<IActionResult> TestProjectDepartmentLinkRepositoryMethods()
        {
            var result = "<h2>Testing Project-Department Link Repository</h2>";

            // Тестирование AddLinkAsync
            var projectId = 1;
            var departmentId = 2;
            await _projectDepartmentLinkRepository.RemoveLinkAsync(projectId, departmentId);
            await _projectDepartmentLinkRepository.AddLinkAsync(projectId, departmentId);
            result += $"Added link between project {projectId} and department {departmentId}<br>";
            result += "<br>------------<br>";

            // Тестирование GetDepartmentIdsByProjectIdAsync
            var departmentIds = await _projectDepartmentLinkRepository.GetDepartmentIdsByProjectIdAsync(projectId);
            result += $"Departments linked to project {projectId}:<br>";
            foreach (var id in departmentIds)
            {
                result += $"Department ID: {id}<br>";
            }

            result += "<br>------------<br>";

            // Тестирование GetProjectIdsByDepartmentIdAsync
            var projectIds = await _projectDepartmentLinkRepository.GetProjectIdsByDepartmentIdAsync(departmentId);
            result += $"Projects linked to department {departmentId}:<br>";
            foreach (var id in projectIds)
            {
                result += $"Project ID: {id}<br>";
            }

            result += "<br>------------<br>";

            // Тестирование RemoveLinkAsync
            await _projectDepartmentLinkRepository.RemoveLinkAsync(projectId, departmentId);
            result += $"Removed link between project {projectId} and department {departmentId}<br>";
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}