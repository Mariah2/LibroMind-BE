using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class AddAddressValidator : AbstractValidator<AddressPostDTO>
    {
        public AddAddressValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty();

            RuleFor(address => address.Number)
                .NotEmpty()
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number.");

            RuleFor(address => address.Floor)
                .GreaterThan(0)
                    .When(a => a.Floor is not null)
                    .WithMessage("{PropertyName} must be a positive number.");

            RuleFor(address => address.Apartment)
                .GreaterThan(0)
                    .When(a => a.Apartment is not null)
                    .WithMessage("{PropertyName} must be a positive number.");

            RuleFor(address => address.City)
                .NotEmpty();

            RuleFor(address => address.County)
                .NotEmpty();

            RuleFor(address => address.Country)
                .NotEmpty();
        }
    }
}
