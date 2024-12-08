using Application.DTOs;
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
        public async Task<IActionResult> AddLinkAsync([FromBody] ProjectAssetLinkDto dto)
        {
            try
            {
                await _projectAssetLinkRepository.AddLinkAsync(dto.projectId, dto.assetId);
                return Ok(new { Message = $"Successfully linked project {dto.projectId} and asset {dto.assetId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveLinkAsync([FromBody] ProjectAssetLinkDto dto)
        {
            try
            {
                await _projectAssetLinkRepository.RemoveLinkAsync(dto.projectId, dto.assetId);
                return Ok(new { Message = $"Successfully removed link between project {dto.projectId} and asset {dto.assetId}" });
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

        [Authorize(Roles = "Admin,Viewer")]
        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> RemoveAssetsByProjectIdAsync(int projectId)
        {
            try
            {
                await _projectAssetLinkRepository.RemoveLinksByProjectIdAsync(projectId);
                return Ok(new { Message = $"Successfully removed links" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }
    }
}
