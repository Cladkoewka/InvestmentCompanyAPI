using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping;

public class EditorProfile : Profile
{
    public EditorProfile()
    {
        CreateMap<Editor, EditorGetDto>();
        CreateMap<EditorCreateDto, Editor>();
        CreateMap<EditorUpdateDto, Editor>();
    }
}