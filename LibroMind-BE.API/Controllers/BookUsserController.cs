﻿using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookUserController : ControllerBase
    {
        private readonly IValidator<BookUserPostDTO> _validator;
        private readonly IBookUserService _bookUserService;

        public BookUserController(IValidator<BookUserPostDTO> validator, IBookUserService bookUserService)
        {
            _validator = validator;
            _bookUserService = bookUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookUseres()
        {
            return Ok(await _bookUserService.FindBookUseresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookUser(int id)
        {
            return Ok(await _bookUserService.FindBookUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBookUser(BookUserPostDTO bookUserToAdd)
        {
            var validationResult = await _validator.ValidateAsync(bookUserToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookUserService.AddBookUser(bookUserToAdd);

            return Ok("BookUser was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookUser(int id, BookUserPostDTO bookUserToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(bookUserToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookUserService.UpdateBookUser(id, bookUserToUpdate);

            return Ok("BookUser was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookUser(int id)
        {
            await _bookUserService.DeleteBookUser(id);

            return Ok("BookUser was deleted successfully!");
        }
    }
}
