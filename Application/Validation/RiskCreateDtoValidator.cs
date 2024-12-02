using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class RiskCreateDtoValidator : AbstractValidator<RiskCreateDto>
{
    public RiskCreateDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Risk type is required.")
            .Length(1, 100).WithMessage("Risk type must be between 1 and 100 characters.");
        
        RuleFor(x => x.Grade)
            .InclusiveBetween(1, 10).WithMessage("Risk grade must be between 1 and 10.");
    }
}

public class RiskUpdateDtoValidator : AbstractValidator<RiskUpdateDto>
{
    public RiskUpdateDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Risk type is required.")
            .Length(1, 100).WithMessage("Risk type must be between 1 and 100 characters.");
        
        RuleFor(x => x.Grade)
            .InclusiveBetween(1, 10).WithMessage("Risk grade must be between 1 and 10.");
    }
}