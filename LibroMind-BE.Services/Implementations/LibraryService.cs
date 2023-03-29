using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LibraryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LibraryGetDTO>> FindLibrariesAsync()
        {
            return _mapper.Map<IEnumerable<LibraryGetDTO>>(await _unitOfWork.LibraryRepository.FindAllAsync());
        }

        public async Task<LibraryGetDTO> FindLibraryByIdAsync(int id)
        {
            var existingLibrary = await _unitOfWork.LibraryRepository.FindByIdAsync(id);

            if (existingLibrary is null)
            {
                throw new BadHttpRequestException("Library not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<LibraryGetDTO>(existingLibrary);
        }

        public async Task<IEnumerable<LibraryDetailsGetDTO>> FindLibraryDetailsAsync()
        {
            return _mapper.Map<IEnumerable<LibraryDetailsGetDTO>>(
                await _unitOfWork.LibraryRepository.FindLibraryDetailsAsync());
        }

        public async Task<IEnumerable<BookDetailsGetDTO>> FindLibraryBooksByIdAsync(int id)
        {
            var existingLibrary = await _unitOfWork.LibraryRepository.FindByIdAsync(id);

            if (existingLibrary is null)
            {
                throw new BadHttpRequestException("Library not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<IEnumerable<BookDetailsGetDTO>>(
                await _unitOfWork.BookRepository.FindLibraryBooksByIdAsync(id));
        }

        public async Task AddLibrary(LibraryPostDTO libraryToAdd)
        {
            if (await _unitOfWork.AddressRepository.FindByIdAsync(libraryToAdd.AddressId) is null)
            {
                throw new BadHttpRequestException("Address not found", StatusCodes.Status404NotFound);
            }

            var newLibrary = _mapper.Map<Library>(libraryToAdd);

            _unitOfWork.LibraryRepository.Add(newLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateLibrary(int id, LibraryPostDTO libraryToUpdate)
        {
            var existingLibrary = await _unitOfWork.LibraryRepository.FindByIdAsync(id);

            if (existingLibrary is null)
            {
                throw new BadHttpRequestException("Library not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(libraryToUpdate, existingLibrary);

            _unitOfWork.LibraryRepository.Update(existingLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteLibrary(int id)
        {
            var existingLibrary = await _unitOfWork.LibraryRepository.FindByIdAsync(id);

            if (existingLibrary is null)
            {
                throw new BadHttpRequestException("Library not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.LibraryRepository.Remove(existingLibrary);

            await _unitOfWork.CommitAsync();
        }
    }
}
