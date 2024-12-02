using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
{
    public ProjectCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .Length(1, 100).WithMessage("Project name must be between 1 and 100 characters.");
        
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Project status is required.");
        
        RuleFor(x => x.Profit)
            .GreaterThanOrEqualTo(0).WithMessage("Profit must be a non-negative value.");
        
        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must be a non-negative value.");
        
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Valid customer ID is required.");
        
        RuleFor(x => x.EditorId)
            .GreaterThan(0).WithMessage("Valid editor ID is required.");
    }
}

public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
{
    public ProjectUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .Length(1, 100).WithMessage("Project name must be between 1 and 100 characters.");
        
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Project status is required.");
        
        RuleFor(x => x.Profit)
            .GreaterThanOrEqualTo(0).WithMessage("Profit must be a non-negative value.");
        
        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must be a non-negative value.");
        
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Valid customer ID is required.");
        
        RuleFor(x => x.EditorId)
            .GreaterThan(0).WithMessage("Valid editor ID is required.");
    }
}