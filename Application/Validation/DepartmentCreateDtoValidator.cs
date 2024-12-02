using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .Length(1, 100).WithMessage("Department name must be between 1 and 100 characters.");
    }
}

public class DepartmentUpdateDtoValidator : AbstractValidator<DepartmentUpdateDto>
{
    public DepartmentUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .Length(1, 100).WithMessage("Department name must be between 1 and 100 characters.");
    }
}