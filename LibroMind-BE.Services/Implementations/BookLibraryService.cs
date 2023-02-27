using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BookLibraryService : IBookLibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookLibraryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookLibraryGetDTO>> FindBookLibraryesAsync()
        {
            return _mapper.Map<IEnumerable<BookLibraryGetDTO>>(await _unitOfWork.BookLibraryRepository.FindAllAsync());
        }

        public async Task<BookLibraryGetDTO> FindBookLibraryByIdAsync(int id)
        {
            var existingBookLibrary = await _unitOfWork.BookLibraryRepository.FindByIdAsync(id);

            if (existingBookLibrary is null)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookLibraryGetDTO>(existingBookLibrary);
        }

        public async Task AddBookLibrary(BookLibraryPostDTO bookLibraryToAdd)
        {
            var newBookLibrary = _mapper.Map<BookLibrary>(bookLibraryToAdd);

            _unitOfWork.BookLibraryRepository.Add(newBookLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBookLibrary(int id, BookLibraryPutDTO bookLibraryToUpdate)
        {
            var existingBookLibrary = await _unitOfWork.BookLibraryRepository.FindByIdAsync(id);

            if (existingBookLibrary is null)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(bookLibraryToUpdate, existingBookLibrary);

            _unitOfWork.BookLibraryRepository.Update(existingBookLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBookLibrary(int id)
        {
            var existingBookLibrary = await _unitOfWork.BookLibraryRepository.FindByIdAsync(id);

            if (existingBookLibrary is null)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.BookLibraryRepository.Remove(existingBookLibrary);

            await _unitOfWork.CommitAsync();
        }
    }
}
