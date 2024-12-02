using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class EditorCreateDtoValidator : AbstractValidator<EditorCreateDto>
{
    public EditorCreateDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .Length(1, 100).WithMessage("Full name must be between 1 and 100 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
    }
}

public class EditorUpdateDtoValidator : AbstractValidator<EditorUpdateDto>
{
    public EditorUpdateDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .Length(1, 100).WithMessage("Full name must be between 1 and 100 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
    }
}