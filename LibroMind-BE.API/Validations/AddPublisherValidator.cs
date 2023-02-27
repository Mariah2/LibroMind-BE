using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddPublisherValidator : AbstractValidator<PublisherPostDTO>
    {
        public AddPublisherValidator() 
        {
            RuleFor(publisher => publisher.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
