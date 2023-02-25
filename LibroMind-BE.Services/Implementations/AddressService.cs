using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressGetDTO>> FindAddressesAsync()
        {
            return _mapper.Map<IEnumerable<AddressGetDTO>>(await _unitOfWork.AddressRepository.FindAllAsync());
        }

        public async Task<AddressGetDTO> FindAddressByIdAsync(int id)
        {
            var existingAddress = await _unitOfWork.AddressRepository.FindByIdAsync(id);

            if (existingAddress is null)
            {
                throw new ArgumentNullException("Address not found!");
            }

            return _mapper.Map<AddressGetDTO>(existingAddress);
        }

        public async Task AddAddress(AddressPostDTO addressToAdd)
        {
            var newAddress = _mapper.Map<Address>(addressToAdd);

            _unitOfWork.AddressRepository.Add(newAddress);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAddress(int id, AddressPostDTO addressToUpdate)
        {
            var existingAddress = await _unitOfWork.AddressRepository.FindByIdAsync(id);

            if (existingAddress is null)
            {
                throw new ArgumentNullException("Address not found!");
            }

            _mapper.Map(addressToUpdate, existingAddress);

            _unitOfWork.AddressRepository.Update(existingAddress);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAddress(int id)
        {
            var existingAddress = await _unitOfWork.AddressRepository.FindByIdAsync(id);

            if (existingAddress is null)
            {
                throw new ArgumentNullException("Address not found!");
            }

            _unitOfWork.AddressRepository.Remove(existingAddress);

            await _unitOfWork.CommitAsync();
        }
    }
}
