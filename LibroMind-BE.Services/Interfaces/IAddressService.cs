using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<IEnumerable<Address>> GetAddressesAsync();
    }
}
