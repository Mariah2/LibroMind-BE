using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddLibraryValidator : AbstractValidator<LibraryGetDTO>
    {
        public AddLibraryValidator() 
        {
            RuleFor(library => library.AddressId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(library => library.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
