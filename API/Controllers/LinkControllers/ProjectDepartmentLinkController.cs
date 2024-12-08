using Application.DTOs;
using Domain.Interfaces.LinkRepositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddLinkAsync([FromBody] ProjectDepartmentLinkDto dto)
        {
            try
            {
                await _projectDepartmentLinkRepository.AddLinkAsync(dto.projectId, dto.departmentId);
                return Ok(new { Message = $"Successfully linked project {dto.projectId} and department {dto.departmentId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveLinkAsync([FromBody] ProjectDepartmentLinkDto dto)
        {
            try
            {
                await _projectDepartmentLinkRepository.RemoveLinkAsync(dto.projectId, dto.departmentId);
                return Ok(new { Message = $"Successfully removed link between project {dto.projectId} and department {dto.departmentId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetDepartmentsByProjectIdAsync(int projectId)
        {
            try
            {
                var departmentIds = await _projectDepartmentLinkRepository.GetDepartmentIdsByProjectIdAsync(projectId);
                return Ok(new { ProjectId = projectId, DepartmentIds = departmentIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve departments", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetProjectsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                var projectIds = await _projectDepartmentLinkRepository.GetProjectIdsByDepartmentIdAsync(departmentId);
                return Ok(new { DepartmentId = departmentId, ProjectIds = projectIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve projects", Error = ex.Message });
            }
        }
        
        [Authorize(Roles = "Admin,Viewer")]
        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> RemoveDepartmentsByProjectIdAsync(int projectId)
        {
            try
            {
                await _projectDepartmentLinkRepository.RemoveDepartmentsByProjectIdAsync(projectId);
                return Ok(new { Message = $"Successfully removed links" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }
    }
}
