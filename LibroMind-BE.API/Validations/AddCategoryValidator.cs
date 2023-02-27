using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddCategoryValidator : AbstractValidator<CategoryPostDTO>
    {
        public AddCategoryValidator() 
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
