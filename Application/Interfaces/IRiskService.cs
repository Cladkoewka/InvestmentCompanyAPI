using Application.DTOs;

namespace Application.Interfaces;

public interface IRiskService : IService<RiskGetDto, RiskCreateDto, RiskUpdateDto>
{
    Task<IEnumerable<RiskGetDto>> GetByGradeAsync(int grade);
}