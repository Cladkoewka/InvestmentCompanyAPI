using Application.DTOs;
using AutoMapper;
using Domain.Models.Links;

namespace Application.Mapping;

public class ProjectRiskLinkProfile : Profile
{
    public ProjectRiskLinkProfile()
    {
        CreateMap<ProjectRiskLinkDto, ProjectRiskLink>();
    }
}