using FluentValidation;
using LibroMind_BE.Services.Models.Put;

namespace LibroMind_BE.API.Validations
{
    public class UpdateUserValidator : AbstractValidator<UserPutDTO>
    {
        public UpdateUserValidator()
        {
        }
    }
}
