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

        // Добавить связь между проектом и департаментом
        [HttpPost]
        public async Task<IActionResult> AddLinkAsync(int projectId, int departmentId)
        {
            try
            {
                await _projectDepartmentLinkRepository.AddLinkAsync(projectId, departmentId);
                return Ok(new { Message = $"Successfully linked project {projectId} and department {departmentId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add link", Error = ex.Message });
            }
        }

        // Удалить связь между проектом и департаментом
        [HttpDelete]
        public async Task<IActionResult> RemoveLinkAsync(int projectId, int departmentId)
        {
            try
            {
                await _projectDepartmentLinkRepository.RemoveLinkAsync(projectId, departmentId);
                return Ok(new { Message = $"Successfully removed link between project {projectId} and department {departmentId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to remove link", Error = ex.Message });
            }
        }

        // Получить список департаментов по ID проекта
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

        // Получить список проектов по ID департамента
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
    }
}
