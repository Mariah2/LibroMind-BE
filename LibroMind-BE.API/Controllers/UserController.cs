using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<UserPostDTO> _validator;
        private readonly IUserService _userService;

        public UserController(IValidator<UserPostDTO> validator, IUserService userService)
        {
            _validator = validator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUseres()
        {
            return Ok(await _userService.FindUseresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.FindUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserPostDTO userToAdd)
        {
            var validationResult = await _validator.ValidateAsync(userToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _userService.AddUser(userToAdd);

            return Ok("User was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPostDTO userToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(userToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _userService.UpdateUser(id, userToUpdate);

            return Ok("User was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return Ok("User was deleted successfully!");
        }
    }
}
