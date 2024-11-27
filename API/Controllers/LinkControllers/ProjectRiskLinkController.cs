using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LinkControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectRiskLinkController : ControllerBase
    {
        private readonly IProjectRiskLinkRepository _projectRiskLinkRepository;

        public ProjectRiskLinkController(IProjectRiskLinkRepository projectRiskLinkRepository)
        {
            _projectRiskLinkRepository = projectRiskLinkRepository;
        }

        // Тестовый метод для проверки работы репозитория
        [HttpGet("test")]
        public async Task<IActionResult> TestProjectRiskLinkRepositoryMethods()
        {
            var result = "<h2>Testing Project-Risk Link Repository</h2>";

            // Тестирование AddLinkAsync
            var projectId = 1;
            var riskId = 5;
            await _projectRiskLinkRepository.RemoveLinkAsync(projectId, riskId);
            await _projectRiskLinkRepository.AddLinkAsync(projectId, riskId);
            result += $"Added link between project {projectId} and risk {riskId}<br>";
            result += "<br>------------<br>";

            // Тестирование GetRiskIdsByProjectIdAsync
            var riskIds = await _projectRiskLinkRepository.GetRiskIdsByProjectIdAsync(projectId);
            result += $"Risks linked to project {projectId}:<br>";
            foreach (var id in riskIds)
            {
                result += $"Risk ID: {id}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetProjectIdsByRiskIdAsync
            var projectIds = await _projectRiskLinkRepository.GetProjectIdsByRiskIdAsync(riskId);
            result += $"Projects linked to risk {riskId}:<br>";
            foreach (var id in projectIds)
            {
                result += $"Project ID: {id}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование RemoveLinkAsync
            await _projectRiskLinkRepository.RemoveLinkAsync(projectId, riskId);
            result += $"Removed link between project {projectId} and risk {riskId}<br>";
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}
