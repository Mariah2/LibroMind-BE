using FluentValidation;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class UpdateBorrowValidator : AbstractValidator<BorrowPutDTO>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateBorrowValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            RuleFor(borrow => borrow.ReturnDate)
                .NotEmpty()
                .LessThanOrEqualTo(_dateTimeProvider.UtcNow);

            RuleFor(borrow => borrow.HasReturnedBook)
                .NotEmpty();
        }
    }
}
