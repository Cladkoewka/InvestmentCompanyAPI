using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class RiskService : IRiskService
    {
        private readonly IRiskRepository _riskRepository;
        private readonly IMapper _mapper;

        public RiskService(IRiskRepository riskRepository, IMapper mapper)
        {
            _riskRepository = riskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RiskGetDto>> GetAllAsync()
        {
            var risks = await _riskRepository.GetAllAsync();
            return risks.Select(risk => _mapper.Map<RiskGetDto>(risk));
        }

        public async Task<RiskGetDto?> GetByIdAsync(int id)
        {
            var risk = await _riskRepository.GetByIdAsync(id);
            if (risk == null)
                return null;

            return _mapper.Map<RiskGetDto>(risk);
        }

        public async Task<IEnumerable<RiskGetDto>> GetByGradeAsync(int grade)
        {
            var risks = await _riskRepository.GetByGradeAsync(grade);
            return risks.Select(risk => _mapper.Map<RiskGetDto>(risk));
        }

        public async Task<RiskGetDto> AddAsync(RiskCreateDto dto)
        {
            var risk = _mapper.Map<Risk>(dto);
            await _riskRepository.AddAsync(risk);

            return _mapper.Map<RiskGetDto>(risk);
        }

        public async Task<bool> UpdateAsync(int id, RiskUpdateDto dto)
        {
            var existingRisk = await _riskRepository.GetByIdAsync(id);
            if (existingRisk == null)
                return false;

            // Маппим DTO в сущность, сохраняя существующий объект
            _mapper.Map(dto, existingRisk);
            await _riskRepository.UpdateAsync(existingRisk);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRisk = await _riskRepository.GetByIdAsync(id);
            if (existingRisk == null)
                return false;

            await _riskRepository.DeleteAsync(existingRisk);
            return true;
        }
    }
}
