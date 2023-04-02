using FluentValidation;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IValidator<AddressPostDTO> _validator;
        private readonly IAddressService _addressService;

        public AddressController(IValidator<AddressPostDTO> validator, IAddressService addressService)
        {
            _validator = validator;
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            return Ok(await _addressService.FindAddressesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            return Ok(await _addressService.FindAddressByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAddress(AddressPostDTO addressToAdd)
        {
            var validationResult = await _validator.ValidateAsync(addressToAdd);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _addressService.AddAddress(addressToAdd);

            return Ok("Address was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressPostDTO addressToUpdate)
        {
            var validationResult = await _validator.ValidateAsync(addressToUpdate);

            if (!validationResult.IsValid)
            {
                throw new BadHttpRequestException(
                    "One or more validation errors occured",
                    new ValidationException(validationResult.Errors));
            }

            await _addressService.UpdateAddress(id, addressToUpdate);

            return Ok("Address was updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _addressService.DeleteAddress(id);

            return Ok("Address was deleted successfully!");
        }
    }
}
