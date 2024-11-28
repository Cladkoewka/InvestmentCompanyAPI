using Application.DTOs;
using AutoMapper;
using Domain.Models.Links;

namespace Application.Mapping;

public class ProjectAssetLinkProfile : Profile
{
    public ProjectAssetLinkProfile()
    {
        CreateMap<ProjectAssetLinkDto, ProjectAssetLink>();
    }
}