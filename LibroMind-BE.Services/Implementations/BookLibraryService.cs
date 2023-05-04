using AutoMapper;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BookLibraryService : IBookLibraryService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookLibraryService(IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookLibraryGetDTO>> FindBookLibrariesAsync()
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

        public async Task<IEnumerable<BookLibraryCardGetDTO>> FindBookLibraryCardsByLibraryIdAsync(int libraryId)
        {
            return _mapper.Map<IEnumerable<BookLibraryCardGetDTO>>(await _unitOfWork.BookLibraryRepository.FindBookLibraryCardsByLibraryIdAsync(libraryId));
        }

        public async Task<BookLibraryGetDTO> FindBookLibraryByBookIdAndLibraryIdAsync(int bookId, int libraryId)
        {
            var existingBookLibrary = await _unitOfWork.BookLibraryRepository.FindBookLibraryByBookIdAndLibraryIdAsync(bookId, libraryId);

            if (existingBookLibrary is null)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookLibraryGetDTO>(existingBookLibrary);
        }

        public async Task AddBookLibrary(BookLibraryPostDTO bookLibraryToAdd)
        {
            if (await _unitOfWork.BookRepository.FindByIdAsync(bookLibraryToAdd.BookId) is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.LibraryRepository.FindByIdAsync(bookLibraryToAdd.LibraryId) is null)
            {
                throw new BadHttpRequestException("Library not found", StatusCodes.Status404NotFound);
            }

            var newBookLibrary = _mapper.Map<BookLibrary>(bookLibraryToAdd);

            newBookLibrary.AddedDate = _dateTimeProvider.UtcNow;

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
