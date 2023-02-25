using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<IEnumerable<AddressGetDTO>> FindAddressesAsync();
        public Task<AddressGetDTO> FindAddressByIdAsync(int id);
        public Task AddAddress(AddressPostDTO addressToAdd);
        public Task UpdateAddress(int id, AddressPostDTO addressToUpdate);
        public Task DeleteAddress(int id);
    }
}
