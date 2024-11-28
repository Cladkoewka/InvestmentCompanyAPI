using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping;

public class AssetProfile : Profile
{
    public AssetProfile()
    {
        CreateMap<Asset, AssetGetDto>();
        CreateMap<AssetCreateDto, Asset>();
        CreateMap<AssetUpdateDto, Asset>();
    }
}