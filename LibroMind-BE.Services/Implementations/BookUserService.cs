using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BookUserService : IBookUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookUserGetDTO>> FindBookUseresAsync()
        {
            return _mapper.Map<IEnumerable<BookUserGetDTO>>(await _unitOfWork.BookUserRepository.FindAllAsync());
        }

        public async Task<BookUserGetDTO> FindBookUserByIdAsync(int id)
        {
            var existingBookUser = await _unitOfWork.BookUserRepository.FindByIdAsync(id);

            if (existingBookUser is null)
            {
                throw new BadHttpRequestException("BookUser not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BookUserGetDTO>(existingBookUser);
        }

        public async Task AddBookUser(BookUserPostDTO bookUserToAdd)
        {
            if (await _unitOfWork.BookRepository.FindByIdAsync(bookUserToAdd.BookId) is null)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.UserRepository.FindByIdAsync(bookUserToAdd.UserId) is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            var newBookUser = _mapper.Map<BookUser>(bookUserToAdd);

            _unitOfWork.BookUserRepository.Add(newBookUser);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBookUser(int id, BookUserPostDTO bookUserToUpdate)
        {
            var existingBookUser = await _unitOfWork.BookUserRepository.FindByIdAsync(id);

            if (existingBookUser is null)
            {
                throw new BadHttpRequestException("BookUser not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(bookUserToUpdate, existingBookUser);

            _unitOfWork.BookUserRepository.Update(existingBookUser);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBookUser(int id)
        {
            var existingBookUser = await _unitOfWork.BookUserRepository.FindByIdAsync(id);

            if (existingBookUser is null)
            {
                throw new BadHttpRequestException("BookUser not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.BookUserRepository.Remove(existingBookUser);

            await _unitOfWork.CommitAsync();
        }
    }
}
