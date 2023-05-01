using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookGetDTO>> FindBooksAsync()
        {
            return _mapper.Map<IEnumerable<BookGetDTO>>(await _unitOfWork.BookRepository.FindAllAsync());
        }

        public async Task<IEnumerable<BookCardGetDTO>> FindBookCardsAsync()
        {
            return _mapper.Map<IEnumerable<BookCardGetDTO>>(await _unitOfWork.BookRepository.FindBookCardsAsync());
        }

        public async Task<IEnumerable<BookCardGetDTO>> FindBookCardsByUserIdAsync(int id)
        {
            var existingUser = await _unitOfWork.UserRepository.FindByIdAsync(id);

            if (existingUser is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<IEnumerable<BookCardGetDTO>>(
                await _unitOfWork.BookRepository.FindBookCardsByUserIdAsync(id));
        }

        public async Task<IEnumerable<BookCardGetDTO>> FindBookCardsForLibraryByParamAsync(int id, string searchParam)
        {
            return _mapper.Map<IEnumerable<BookCardGetDTO>>(
                await _unitOfWork.BookRepository.FindBookCardsForLibraryByParamAsync(id, searchParam));
        }

        public async Task<BookGetDTO> FindBookByIdAsync(int id)
        {
            var existingBook = await _unitOfWork.BookRepository.FindByIdAsync(id);

            if (existingBook is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookGetDTO>(existingBook);
        }

        public async Task<BookDetailsGetDTO> FindBookDetailsByIdAsync(int id)
        {
            var existingBook = await _unitOfWork.BookRepository.FindBookDetailsByIdAsync(id);

            if (existingBook is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookDetailsGetDTO>(existingBook);
        }

        public async Task AddBook(BookPostDTO bookToAdd)
        {
            if (await _unitOfWork.AuthorRepository.FindByIdAsync(bookToAdd.AuthorId) is null)
            {
                throw new BadHttpRequestException("Author not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.PublisherRepository.FindByIdAsync(bookToAdd.PublisherId) is null)
            {
                throw new BadHttpRequestException("Publisher not found", StatusCodes.Status404NotFound);
            }

            var newBook = _mapper.Map<Book>(bookToAdd);

            newBook.Rating = 0;

            _unitOfWork.BookRepository.Add(newBook);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBook(int id, BookPostDTO bookToUpdate)
        {
            var existingBook = await _unitOfWork.BookRepository.FindByIdAsync(id);

            if (existingBook is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(bookToUpdate, existingBook);

            _unitOfWork.BookRepository.Update(existingBook);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBook(int id)
        {
            var existingBook = await _unitOfWork.BookRepository.FindByIdAsync(id);

            if (existingBook is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.BookRepository.Remove(existingBook);

            await _unitOfWork.CommitAsync();
        }
    }
}
