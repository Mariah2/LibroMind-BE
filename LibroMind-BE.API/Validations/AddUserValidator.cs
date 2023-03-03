using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddUserValidator : AbstractValidator<UserPostDTO>
    {
        public AddUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .Matches("/^[A-Z]{1}[a-z ,.'-]+$/i").WithMessage("Please use only letters for your {PropertyName}");

            RuleFor(user => user.LastName)
                .NotEmpty()
                .Matches("/^[A-Z]{1}[a-z ,.'-]+$/i").WithMessage("Please use only letters for your {PropertyName}");

            RuleFor(user => user.BirthDate)
                .NotEmpty();

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(user => user.Phone)
                .NotEmpty()
                .Matches(@"^\(?([0]{1})\)?([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$")
                    .WithMessage("{PropertyName} must be look like this '0722-123-123', '0722.123.123' or '0722 123 123'");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
