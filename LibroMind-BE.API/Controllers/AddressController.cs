using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Interfaces;
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
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _addressService.GetAddressesAsync();
        }
    }
}
