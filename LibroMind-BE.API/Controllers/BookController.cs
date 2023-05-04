using FluentValidation;
using LibroMind_BE.Services.Implementations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IValidator<BookPostDTO> _validator;
        private readonly IBookService _bookService;

        public BookController(IValidator<BookPostDTO> validator, IBookService bookService)
        {
            _validator = validator;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.FindBooksAsync());
        }

        [HttpGet("cards")]
        public async Task<IActionResult> GetBookCards()
        {
            return Ok(await _bookService.FindBookCardsAsync());
        }


        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetBookCardsByUserId(int id)
        {
            return Ok(await _bookService.FindBookCardsByUserIdAsync(id));
        }

        [HttpGet("cards/library/{id}/filter-by")]
        public async Task<IActionResult> GetBookCardsForLibraryByParam(int id, string? searchParam)
        {
            return Ok(await _bookService.FindBookCardsForLibraryByParamAsync(id, searchParam ?? ""));
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetBookDetailsById(int id)
        {
            return Ok(await _bookService.FindBookDetailsByIdAsync(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookService.FindBookByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(BookPostDTO bookToAdd)
        {
            var validationResult = await _validator.ValidateAsync(bookToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookService.AddBook(bookToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookPostDTO bookToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(bookToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookService.UpdateBook(id, bookToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);

            return Ok();
        }
    }
}
