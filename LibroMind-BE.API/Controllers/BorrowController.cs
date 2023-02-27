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
        public async Task<IActionResult> GetBorrowes()
        {
            return Ok(await _borrowService.FindBorrowesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrow(int id)
        {
            return Ok(await _borrowService.FindBorrowByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBorrow(BorrowPostDTO borrowToAdd)
        {
            var validationResult = await _validatorPost.ValidateAsync(borrowToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _borrowService.AddBorrow(borrowToAdd);

            return Ok("Borrow was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrow(int id, BorrowPutDTO borrowToUpdate)
        {
            var validationResult = await _validatorPut.ValidateAsync(borrowToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _borrowService.UpdateBorrow(id, borrowToUpdate);

            return Ok("Borrow was updated successfully!");
        }

        [HttpPut("{id}/extend")]
        public async Task<IActionResult> ExtendBorrow(int id)
        {

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrow(int id)
        {
            await _borrowService.DeleteBorrow(id);

            return Ok("Borrow was deleted successfully!");
        }
    }
}
