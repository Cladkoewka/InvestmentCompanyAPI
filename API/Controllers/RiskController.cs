using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskController : ControllerBase
    {
        private readonly IRiskRepository _riskRepository;

        public RiskController(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestRiskRepositoryMethods()
        {
            var result = string.Empty;

            // Тестирование GetByIdAsync
            var riskById = await _riskRepository.GetByIdAsync(1);
            result += riskById is not null 
                ? $"Risk by ID: {riskById.Type}, Grade: {riskById.Grade}" 
                : "Risk not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allRisks = await _riskRepository.GetAllAsync();
            result += "<br>All risks:<br>";
            foreach (var risk in allRisks)
            {
                result += $"Risk: {risk.Type}, Grade: {risk.Grade}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newRisk = new Risk { Type = "Operational", Grade = 3 };
            await _riskRepository.AddAsync(newRisk);
            result += "Added new risk<br>";
            
            allRisks = await _riskRepository.GetAllAsync();
            result += "<br>All risks after adding:<br>";
            foreach (var risk in allRisks)
            {
                result += $"Risk: {risk.Type}, Grade: {risk.Grade}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var riskToUpdate = new Risk { Id = 2, Type = "Financial", Grade = 4 };
            await _riskRepository.UpdateAsync(riskToUpdate);
            result += "Updated risk<br>";
            
            allRisks = await _riskRepository.GetAllAsync();
            result += "<br>All risks after update:<br>";
            foreach (var risk in allRisks)
            {
                result += $"Risk: {risk.Type}, Grade: {risk.Grade}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _riskRepository.DeleteAsync(riskToUpdate);
            result += "Deleted risk<br>";
            
            allRisks = await _riskRepository.GetAllAsync();
            result += "<br>All risks after deletion:<br>";
            foreach (var risk in allRisks)
            {
                result += $"Risk: {risk.Type}, Grade: {risk.Grade}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByGradeAsync
            var risksByGrade = await _riskRepository.GetByGradeAsync(3);
            result += "Risks with grade 3:<br>";
            foreach (var risk in risksByGrade)
            {
                result += $"Risk: {risk.Type}, Grade: {risk.Grade}<br>";
            }
            result += "<br>------------<br>";

            return Content(result, "text/html");
        }
    }
}
