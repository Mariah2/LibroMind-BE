using FluentValidation;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddBookValidator : AbstractValidator<BookPostDTO>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddBookValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            RuleFor(book => book.AuthorId)
                .NotEmpty();

            RuleFor(book => book.PublisherId)
                .NotEmpty();

            RuleFor(book => book.Title)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(book => book.PublishingDate)
                .NotEmpty()
                .LessThan(_dateTimeProvider.UtcNow).WithMessage("A book cannot be published in the future.");

            RuleFor(book => book.Description)
                .NotEmpty();

            RuleFor(book => book.PagesNumber)
                .NotEmpty()
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number.");
        }
    }
}
