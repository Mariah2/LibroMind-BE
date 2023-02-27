using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddBookCategoryValidator : AbstractValidator<BookCategoryPostDTO>
    {
        public AddBookCategoryValidator() 
        {
            RuleFor(bookCategory => bookCategory.BookId)
                .NotEmpty();

            RuleFor(bookCategory => bookCategory.CategoryId)
                .NotEmpty();
        }
    }
}
