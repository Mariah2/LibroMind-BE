using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IValidator<AuthorPostDTO> _validator;
        private readonly IAuthorService _authorService;

        public AuthorController(IValidator<AuthorPostDTO> validator, IAuthorService authorService)
        {
            _validator = validator;
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthores()
        {
            return Ok(await _authorService.FindAuthoresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            return Ok(await _authorService.FindAuthorByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAuthor(AuthorPostDTO authorToAdd)
        {
            var validationResult = await _validator.ValidateAsync(authorToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _authorService.AddAuthor(authorToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorPostDTO authorToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(authorToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _authorService.UpdateAuthor(id, authorToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);

            return Ok();
        }
    }
}
