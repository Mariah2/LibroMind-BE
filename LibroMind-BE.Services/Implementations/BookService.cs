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

        public async Task<IEnumerable<BookGetDTO>> FindBookesAsync()
        {
            return _mapper.Map<IEnumerable<BookGetDTO>>(await _unitOfWork.BookRepository.FindAllAsync());
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

        public async Task AddBook(BookPostDTO bookToAdd)
        {
            var newBook = _mapper.Map<Book>(bookToAdd);

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
