using FluentValidation;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddBorrowValidator : AbstractValidator<BorrowPostDTO>
    {
        public AddBorrowValidator() 
        {
            RuleFor(borrow => borrow.UserId)
                .NotEmpty();

            RuleFor(borrow => borrow.BookLibraryId)
                .NotEmpty();
        }
    }
}
