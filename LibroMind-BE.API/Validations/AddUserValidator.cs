using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddUserValidator : AbstractValidator<UserPostDTO>
    {
        public AddUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(user => user.BirthDate)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(user => user.Email)
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(user => user.Phone)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
