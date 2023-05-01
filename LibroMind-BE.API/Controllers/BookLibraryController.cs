using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLibraryController : ControllerBase
    {
        private readonly IValidator<BookLibraryPostDTO> _validatorPost;
        private readonly IValidator<BookLibraryPutDTO> _validatorPut;
        private readonly IBookLibraryService _bookLibraryService;

        public BookLibraryController(
            IValidator<BookLibraryPostDTO> validatorPost,
            IValidator<BookLibraryPutDTO> validatorPut,
            IBookLibraryService bookLibraryService)
        {
            _validatorPost = validatorPost;
            _validatorPut = validatorPut;
            _bookLibraryService = bookLibraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookLibraries()
        {
            return Ok(await _bookLibraryService.FindBookLibrariesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookLibrary(int id)
        {
            return Ok(await _bookLibraryService.FindBookLibraryByIdAsync(id));
        }

        [HttpGet("book/{bookId}/library/{libraryId}")]
        public async Task<IActionResult> GetBookLibraryByBookIdAndLibraryId(int bookId, int libraryId)
        {
            return Ok(await _bookLibraryService.FindBookLibraryByBookIdAndLibraryIdAsync(bookId, libraryId));
        }

        [HttpPost]
        public async Task<IActionResult> PostBookLibrary(BookLibraryPostDTO bookLibraryToAdd)
        {
            var validationResult = await _validatorPost.ValidateAsync(bookLibraryToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookLibraryService.AddBookLibrary(bookLibraryToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookLibrary(int id, BookLibraryPutDTO bookLibraryToUpdate)
        {
            var validationResult = await _validatorPut.ValidateAsync(bookLibraryToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookLibraryService.UpdateBookLibrary(id, bookLibraryToUpdate);

            return Ok("BookLibrary was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookLibrary(int id)
        {
            await _bookLibraryService.DeleteBookLibrary(id);

            return Ok("BookLibrary was deleted successfully!");
        }
    }
}
