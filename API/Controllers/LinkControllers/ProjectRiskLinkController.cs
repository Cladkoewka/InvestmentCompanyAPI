using Application.DTOs;
using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddLinkAsync([FromBody] ProjectRiskLinkDto dto)
        {
            try
            {
                await _projectRiskLinkRepository.AddLinkAsync(dto.projectId, dto.riskId);
                return Ok(new { Message = $"Successfully linked project {dto.projectId} and risk {dto.riskId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveLinkAsync([FromBody] ProjectRiskLinkDto dto)
        {
            try
            {
                await _projectRiskLinkRepository.RemoveLinkAsync(dto.projectId, dto.riskId);
                return Ok(new { Message = $"Successfully removed link between project {dto.projectId} and risk {dto.riskId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetRisksByProjectIdAsync(int projectId)
        {
            try
            {
                var riskIds = await _projectRiskLinkRepository.GetRiskIdsByProjectIdAsync(projectId);
                return Ok(new { ProjectId = projectId, RiskIds = riskIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve risks", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("risk/{riskId}")]
        public async Task<IActionResult> GetProjectsByRiskIdAsync(int riskId)
        {
            try
            {
                var projectIds = await _projectRiskLinkRepository.GetProjectIdsByRiskIdAsync(riskId);
                return Ok(new { RiskId = riskId, ProjectIds = projectIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve projects", Error = ex.Message });
            }
        }
        
        [Authorize(Roles = "Admin,Viewer")]
        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> RemoveRisksByProjectIdAsync(int projectId)
        {
            try
            {
                await _projectRiskLinkRepository.RemoveRisksByProjectIdAsync(projectId);
                return Ok(new { Message = $"Successfully removed links" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }
    }
}
