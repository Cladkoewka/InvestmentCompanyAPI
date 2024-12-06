using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddLinkAsync(int projectId, int assetId)
        {
            try
            {
                await _projectAssetLinkRepository.AddLinkAsync(projectId, assetId);
                return Ok(new { Message = $"Successfully linked project {projectId} and asset {assetId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add link", Error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLinkAsync(int projectId, int assetId)
        {
            try
            {
                await _projectAssetLinkRepository.RemoveLinkAsync(projectId, assetId);
                return Ok(new { Message = $"Successfully removed link between project {projectId} and asset {assetId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetAssetsByProjectIdAsync(int projectId)
        {
            try
            {
                var assetIds = await _projectAssetLinkRepository.GetAssetIdsByProjectIdAsync(projectId);
                return Ok(new { ProjectId = projectId, AssetIds = assetIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve assets", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("asset/{assetId}")]
        public async Task<IActionResult> GetProjectsByAssetIdAsync(int assetId)
        {
            try
            {
                var projectIds = await _projectAssetLinkRepository.GetProjectIdsByAssetIdAsync(assetId);
                return Ok(new { AssetId = assetId, ProjectIds = projectIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve projects", Error = ex.Message });
            }
        }
    }
}
