using FluentValidation;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddAuthorValidator : AbstractValidator<AuthorPostDTO>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddAuthorValidator(IDateTimeProvider dateTimeProvider) 
        {
            _dateTimeProvider = dateTimeProvider;

            RuleFor(author => author.FirstName)
                .NotEmpty()
                .Matches("/^[a-z ,.'-]+$/i").WithMessage("Please use only letters for your {PropertyName}");

            RuleFor(author => author.LastName)
                .NotEmpty()
                .Matches("/^[a-z ,.'-]+$/i").WithMessage("Please use only letters for your {PropertyName}");

            RuleFor(author => author.BirthDate)
                .NotEmpty()
                .LessThan(_dateTimeProvider.UtcNow).WithMessage("An author cannot be born in the future.");
        }
    }
}
