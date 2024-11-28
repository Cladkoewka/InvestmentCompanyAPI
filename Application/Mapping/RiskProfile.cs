using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping;

public class RiskProfile : Profile
{
    public RiskProfile()
    {
        CreateMap<Risk, RiskGetDto>();
        CreateMap<RiskCreateDto, Risk>();
        CreateMap<RiskUpdateDto, Risk>();
    }
}