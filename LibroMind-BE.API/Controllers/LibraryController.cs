using FluentValidation;
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
        public async Task<IActionResult> GetLibraryes()
        {
            return Ok(await _libraryService.FindLibraryesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibrary(int id)
        {
            return Ok(await _libraryService.FindLibraryByIdAsync(id));
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

            return Ok("Library was added successfully!");
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

            return Ok("Library was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            await _libraryService.DeleteLibrary(id);

            return Ok("Library was deleted successfully!");
        }
    }
}
