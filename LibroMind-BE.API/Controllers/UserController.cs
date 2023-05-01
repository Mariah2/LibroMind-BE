using FluentValidation;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models.Put;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<UserPutDTO> _validatorPut;
        private readonly IUserService _userService;

        public UserController(IValidator<UserPutDTO> validatorPut, IUserService userService)
        {
            _validatorPut = validatorPut;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.FindUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.FindUserByIdAsync(id));
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            return Ok(await _userService.FindUserProfileByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPutDTO userToUpdate)
        {
            var validationResult = await _validatorPut.ValidateAsync(userToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _userService.UpdateUser(id, userToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }
    }
}
