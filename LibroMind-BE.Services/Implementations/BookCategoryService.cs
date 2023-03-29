using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookCategoryGetDTO>> FindBookCategoryesAsync()
        {
            return _mapper.Map<IEnumerable<BookCategoryGetDTO>>(await _unitOfWork.BookCategoryRepository.FindAllAsync());
        }

        public async Task<BookCategoryGetDTO> FindBookCategoryByIdAsync(int id)
        {
            var existingBookCategory = await _unitOfWork.BookCategoryRepository.FindByIdAsync(id);

            if (existingBookCategory is null)
            {
                throw new BadHttpRequestException("BookCategory not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookCategoryGetDTO>(existingBookCategory);
        }

        public async Task AddBookCategory(BookCategoryPostDTO bookCategoryToAdd)
        {
            if (await _unitOfWork.BookRepository.FindByIdAsync(bookCategoryToAdd.BookId) is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.CategoryRepository.FindByIdAsync(bookCategoryToAdd.CategoryId) is null)
            {
                throw new BadHttpRequestException("Category not found", StatusCodes.Status404NotFound);
            }

            var newBookCategory = _mapper.Map<BookCategory>(bookCategoryToAdd);

            _unitOfWork.BookCategoryRepository.Add(newBookCategory);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBookCategory(int id, BookCategoryPostDTO bookCategoryToUpdate)
        {
            var existingBookCategory = await _unitOfWork.BookCategoryRepository.FindByIdAsync(id);

            if (existingBookCategory is null)
            {
                throw new BadHttpRequestException("BookCategory not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(bookCategoryToUpdate, existingBookCategory);

            _unitOfWork.BookCategoryRepository.Update(existingBookCategory);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBookCategory(int id)
        {
            var existingBookCategory = await _unitOfWork.BookCategoryRepository.FindByIdAsync(id);

            if (existingBookCategory is null)
            {
                throw new BadHttpRequestException("BookCategory not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.BookCategoryRepository.Remove(existingBookCategory);

            await _unitOfWork.CommitAsync();
        }
    }
}
