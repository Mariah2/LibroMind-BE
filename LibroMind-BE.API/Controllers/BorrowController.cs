using FluentValidation;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IValidator<BorrowPostDTO> _validatorPost;
        private readonly IValidator<BorrowPutDTO> _validatorPut;
        private readonly IBorrowService _borrowService;

        public BorrowController(
            IValidator<BorrowPostDTO> validatorPost,
            IValidator<BorrowPutDTO> validatorPut,
            IBorrowService borrowService)
        {
            _validatorPost = validatorPost;
            _validatorPut = validatorPut;
            _borrowService = borrowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowings()
        {
            return Ok(await _borrowService.FindBorrowingsAsync());
        }

        [HttpGet("libraries/{libraryId}")]
        public async Task<IActionResult> GetBorrowingsByLibraryId(int libraryId)
        {
            return Ok(await _borrowService.FindBorrowingsByLibraryIdAsync(libraryId));
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetBorrowingsByUserId(int userId)
        {
            return Ok(await _borrowService.FindBorrowingsByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowing(int id)
        {
            return Ok(await _borrowService.FindBorrowingByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBorrowing(BorrowPostDTO borrowToAdd)
        {
            var validationResult = await _validatorPost.ValidateAsync(borrowToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _borrowService.AddBorrowingAsync(borrowToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowing(int id, BorrowPutDTO borrowToUpdate)
        {
            var validationResult = await _validatorPut.ValidateAsync(borrowToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _borrowService.UpdateBorrowingAsync(id, borrowToUpdate);

            return Ok();
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptBorrowing(int id)
        {
            await _borrowService.AcceptBorrowingAsync(id);

            return Ok();
        }

        [HttpPut("{id}/extend")]
        public async Task<IActionResult> ExtendBorrowing(int id)
        {
            await _borrowService.ExtendBorrowingAsync(id);

            return Ok();
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBorrowing(int id)
        {
            await _borrowService.ReturnBorrowingAsync(id);

            return Ok();
        }

        [HttpPut("{id}/request-extension")]
        public async Task<IActionResult> RequestExtensionForBorrowing(int id)
        {
            await _borrowService.RequestExtensionForBorrowingAsync(id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            await _borrowService.DeleteBorrowingAsync(id);

            return Ok();
        }
    }
}
