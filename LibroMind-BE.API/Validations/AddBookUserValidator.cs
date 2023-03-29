using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddBookUserValidator : AbstractValidator<BookUserPostDTO>
    {
        public AddBookUserValidator()
        {
            RuleFor(bookUser => bookUser.BookId)
                .NotEmpty();

            RuleFor(bookUser => bookUser.UserId)
                .NotEmpty();
        }
    }
}
