using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IValidator<CategoryPostDTO> _validator;
        private readonly ICategoryService _controllerService;

        public CategoryController(IValidator<CategoryPostDTO> validator, ICategoryService controllerService)
        {
            _validator = validator;
            _controllerService = controllerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryes()
        {
            return Ok(await _controllerService.FindCategoryesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await _controllerService.FindCategoryByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryPostDTO controllerToAdd)
        {
            var validationResult = await _validator.ValidateAsync(controllerToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _controllerService.AddCategory(controllerToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryPostDTO controllerToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(controllerToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _controllerService.UpdateCategory(id, controllerToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _controllerService.DeleteCategory(id);

            return Ok();
        }
    }
}
