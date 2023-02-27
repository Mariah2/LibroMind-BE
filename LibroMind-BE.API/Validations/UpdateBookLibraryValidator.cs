﻿using FluentValidation;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.API.Validations
{
    public class UpdateBookLibraryValidator : AbstractValidator<BookLibraryPutDTO>
    {
        public UpdateBookLibraryValidator() { }
    }
}
