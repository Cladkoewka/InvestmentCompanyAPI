using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
{
    public EmployeeCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 50).WithMessage("First name must be between 1 and 50 characters.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(1, 50).WithMessage("Last name must be between 1 and 50 characters.");
        
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Valid department ID is required.");
    }
}

public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
{
    public EmployeeUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 50).WithMessage("First name must be between 1 and 50 characters.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(1, 50).WithMessage("Last name must be between 1 and 50 characters.");
        
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Valid department ID is required.");
    }
}