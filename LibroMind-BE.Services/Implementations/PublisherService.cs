using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublisherGetDTO>> FindPublisheresAsync()
        {
            return _mapper.Map<IEnumerable<PublisherGetDTO>>(await _unitOfWork.PublisherRepository.FindAllAsync());
        }

        public async Task<PublisherGetDTO> FindPublisherByIdAsync(int id)
        {
            var existingPublisher = await _unitOfWork.PublisherRepository.FindByIdAsync(id);

            if (existingPublisher is null)
            {
                throw new BadHttpRequestException("Publisher not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<PublisherGetDTO>(existingPublisher);
        }

        public async Task AddPublisher(PublisherPostDTO addressToAdd)
        {
            var newPublisher = _mapper.Map<Publisher>(addressToAdd);

            _unitOfWork.PublisherRepository.Add(newPublisher);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePublisher(int id, PublisherPostDTO addressToUpdate)
        {
            var existingPublisher = await _unitOfWork.PublisherRepository.FindByIdAsync(id);

            if (existingPublisher is null)
            {
                throw new BadHttpRequestException("Publisher not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(addressToUpdate, existingPublisher);

            _unitOfWork.PublisherRepository.Update(existingPublisher);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePublisher(int id)
        {
            var existingPublisher = await _unitOfWork.PublisherRepository.FindByIdAsync(id);

            if (existingPublisher is null)
            {
                throw new BadHttpRequestException("Publisher not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.PublisherRepository.Remove(existingPublisher);

            await _unitOfWork.CommitAsync();
        }
    }
}
