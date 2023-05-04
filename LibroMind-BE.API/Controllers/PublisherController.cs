using FluentValidation;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IValidator<PublisherPostDTO> _validator;
        private readonly IPublisherService _publisherService;

        public PublisherController(IValidator<PublisherPostDTO> validator, IPublisherService publisherService)
        {
            _validator = validator;
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublisheres()
        {
            return Ok(await _publisherService.FindPublisheresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            return Ok(await _publisherService.FindPublisherByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostPublisher(PublisherPostDTO publisherToAdd)
        {
            var validationResult = await _validator.ValidateAsync(publisherToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _publisherService.AddPublisher(publisherToAdd);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherPostDTO publisherToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(publisherToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _publisherService.UpdatePublisher(id, publisherToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await _publisherService.DeletePublisher(id);

            return Ok();
        }
    }
}
