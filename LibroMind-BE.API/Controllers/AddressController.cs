using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var response = await _addressService.FindAddressesAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            try
            {
                var response = await _addressService.FindAddressByIdAsync(id);

                return Ok(response);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAddress(AddressPostDTO addressToAdd)
        {
            await _addressService.AddAddress(addressToAdd);

            return Ok("Address was added successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressPostDTO addressToUpdate)
        {
            try
            {
                await _addressService.UpdateAddress(id, addressToUpdate);

                return Ok("Address was updated successfully!");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                await _addressService.DeleteAddress(id);

                return Ok("Address was deleted successfully!");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
