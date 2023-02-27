using FluentValidation;
using LibroMind_BE.API.Validations;
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
        public async Task<IActionResult> GetBookes()
        {
            return Ok(await _bookService.FindBookesAsync());
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

            return Ok("Book was added successfully!");
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

            return Ok("Book was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);

            return Ok("Book was deleted successfully!");
        }
    }
}
