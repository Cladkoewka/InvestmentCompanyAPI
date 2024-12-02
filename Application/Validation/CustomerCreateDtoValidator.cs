using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
{
    public CustomerCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .Length(1, 100).WithMessage("Customer name must be between 1 and 100 characters.");
    }
}

public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
{
    public CustomerUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .Length(1, 100).WithMessage("Customer name must be between 1 and 100 characters.");
    }
}