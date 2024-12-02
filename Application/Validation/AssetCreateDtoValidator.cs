using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class AssetCreateDtoValidator : AbstractValidator<AssetCreateDto>
{
    public AssetCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Asset name is required.")
            .Length(1, 100).WithMessage("Asset name must be between 1 and 100 characters.");
    }
}

public class AssetUpdateDtoValidator : AbstractValidator<AssetUpdateDto>
{
    public AssetUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Asset name is required.")
            .Length(1, 100).WithMessage("Asset name must be between 1 and 100 characters.");
    }
}