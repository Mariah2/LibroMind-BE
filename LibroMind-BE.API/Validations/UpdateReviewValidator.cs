using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class UpdateReviewValidator : AbstractValidator<ReviewPutDTO>
    {
        public UpdateReviewValidator()
        {
            RuleFor(review => review.Rating)
                .NotEmpty()
                .LessThanOrEqualTo(10).WithMessage("{PropertyName} must be between 1 and 10")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be between 1 and 10");
        }
    }
}
