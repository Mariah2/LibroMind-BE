using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddBookLibraryValidator : AbstractValidator<BookLibraryPostDTO>
    {
         public AddBookLibraryValidator()
        {
            RuleFor(bookLibrary => bookLibrary.BookId)
                .NotEmpty();

            RuleFor(bookLibrary => bookLibrary.LibraryId)
                .NotEmpty();

            RuleFor(bookLibrary => bookLibrary.Quantity)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
