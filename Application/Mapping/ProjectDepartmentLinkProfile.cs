using Application.DTOs;
using AutoMapper;
using Domain.Models.Links;

namespace Application.Mapping;

public class ProjectDepartmentLinkProfile : Profile
{
    public ProjectDepartmentLinkProfile()
    {
        CreateMap<ProjectDepartmentLinkDto, ProjectDepartmentLink>();
    }
}