using FluentValidation;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IValidator<ReviewPostDTO> _validatorPost;
        private readonly IReviewService _reviewService;

        public ReviewController(
            IValidator<ReviewPostDTO> validatorPost,
            IReviewService reviewService)
        {
            _validatorPost = validatorPost;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewes()
        {
            return Ok(await _reviewService.FindReviewesDetailsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            return Ok(await _reviewService.FindReviewByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostReview(ReviewPostDTO reviewToAdd)
        {
            var validationResult = await _validatorPost.ValidateAsync(reviewToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _reviewService.AddReview(reviewToAdd);

            return Ok("Review was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewPostDTO reviewToUpdate)
        {
            var validationResult = await _validatorPost.ValidateAsync(reviewToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _reviewService.UpdateReview(id, reviewToUpdate);

            return Ok("Review was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.DeleteReview(id);

            return Ok("Review was deleted successfully!");
        }
    }
}
