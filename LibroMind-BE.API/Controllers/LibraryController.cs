﻿using FluentValidation;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IValidator<LibraryPostDTO> _validator;
        private readonly ILibraryService _libraryService;

        public LibraryController(IValidator<LibraryPostDTO> validator, ILibraryService libraryService)
        {
            _validator = validator;
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLibraries()
        {
            return Ok(await _libraryService.FindLibrariesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibrary(int id)
        {
            return Ok(await _libraryService.FindLibraryByIdAsync(id));
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetLibraryDetails()
        {
            return Ok(await _libraryService.FindLibraryDetailsAsync());
        }


        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetLibraryBooks(int id)
        {
            return Ok(await _libraryService.FindLibraryBooksByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostLibrary(LibraryPostDTO libraryToAdd)
        {
            var validationResult = await _validator.ValidateAsync(libraryToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _libraryService.AddLibrary(libraryToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibrary(int id, LibraryPostDTO libraryToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(libraryToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _libraryService.UpdateLibrary(id, libraryToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            await _libraryService.DeleteLibrary(id);

            return Ok();
        }
    }
}
