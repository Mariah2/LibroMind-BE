using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IValidator<BookCategoryPostDTO> _validator;
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoryController(IValidator<BookCategoryPostDTO> validator, IBookCategoryService bookCategoryService)
        {
            _validator = validator;
            _bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookCategoryes()
        {
            return Ok(await _bookCategoryService.FindBookCategoryesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCategory(int id)
        {
            return Ok(await _bookCategoryService.FindBookCategoryByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBookCategory(BookCategoryPostDTO bookCategoryToAdd)
        {
            var validationResult = await _validator.ValidateAsync(bookCategoryToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookCategoryService.AddBookCategory(bookCategoryToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookCategory(int id, BookCategoryPostDTO bookCategoryToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(bookCategoryToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _bookCategoryService.UpdateBookCategory(id, bookCategoryToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCategory(int id)
        {
            await _bookCategoryService.DeleteBookCategory(id);

            return Ok();
        }
    }
}
