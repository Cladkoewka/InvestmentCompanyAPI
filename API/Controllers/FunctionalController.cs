using Application.DTOs.Functional;
using Infrastructure.Repositories.WithoutORM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FunctionalController : ControllerBase
{
    private readonly FunctionalRepository _functionalRepository;

    public FunctionalController(FunctionalRepository functionalRepository)
    {
        _functionalRepository = functionalRepository;
    }

    // Scalar Function
    [Authorize(Roles = "Admin")]
    [HttpGet("totalprofit")]
    public async Task<ActionResult<decimal>> GetTotalProfit()
    {
        try
        {
            var profit = await _functionalRepository.GetTotalProfitAsync();
            return Ok(new { Profit = profit });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while fetching the message", Details = ex.Message });
        }
    }
    
    // Projects by customerId Table Function
    [Authorize(Roles = "Admin")]
    [HttpGet("projects/{customerName}")]
    public async Task<ActionResult<FunctionalProjectDto>> GetProjectsByCustomerId(string customerName)
    {
        try
        {
            var projects = await _functionalRepository.GetProjectsByCustomerNameAsync(customerName);
                
            if (projects == null || projects.Count == 0)
            {
                return NotFound(new { Message = "No projects found for the given customer ID." });
            }

            return Ok(projects);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while fetching the projects", Details = ex.Message });
        }
    }
}