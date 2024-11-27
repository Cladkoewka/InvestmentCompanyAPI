using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LinkControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectAssetLinkController : ControllerBase
    {
        private readonly IProjectAssetLinkRepository _projectAssetLinkRepository;

        public ProjectAssetLinkController(IProjectAssetLinkRepository projectAssetLinkRepository)
        {
            _projectAssetLinkRepository = projectAssetLinkRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestProjectAssetLinkRepositoryMethods()
        {
            var result = "<h2>Testing Project-Asset Link Repository</h2>";

            
            // Тестирование AddLinkAsync
            var projectId = 1;
            var assetId = 5;
            await _projectAssetLinkRepository.RemoveLinkAsync(projectId, assetId);
            await _projectAssetLinkRepository.AddLinkAsync(projectId, assetId);
            result += $"Added link between project {projectId} and asset {assetId}<br>";
            result += "<br>------------<br>";

            // Тестирование GetAssetIdsByProjectIdAsync
            var assetIds = await _projectAssetLinkRepository.GetAssetIdsByProjectIdAsync(projectId);
            result += $"Assets linked to project {projectId}:<br>";
            foreach (var id in assetIds)
            {
                result += $"Asset ID: {id}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetProjectIdsByAssetIdAsync
            var projectIds = await _projectAssetLinkRepository.GetProjectIdsByAssetIdAsync(assetId);
            result += $"Projects linked to asset {assetId}:<br>";
            foreach (var id in projectIds)
            {
                result += $"Project ID: {id}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование RemoveLinkAsync
            await _projectAssetLinkRepository.RemoveLinkAsync(projectId, assetId);
            result += $"Removed link between project {projectId} and asset {assetId}<br>";
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}
