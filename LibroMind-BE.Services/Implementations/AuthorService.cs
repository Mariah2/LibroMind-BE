using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorGetDTO>> FindAuthoresAsync()
        {
            return _mapper.Map<IEnumerable<AuthorGetDTO>>(await _unitOfWork.AuthorRepository.FindAllAsync());
        }

        public async Task<AuthorGetDTO> FindAuthorByIdAsync(int id)
        {
            var existingAuthor = await _unitOfWork.AuthorRepository.FindByIdAsync(id);

            if (existingAuthor is null)
            {
                throw new BadHttpRequestException("Author not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<AuthorGetDTO>(existingAuthor);
        }

        public async Task AddAuthor(AuthorPostDTO authorToAdd)
        {
            var newAuthor = _mapper.Map<Author>(authorToAdd);

            _unitOfWork.AuthorRepository.Add(newAuthor);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAuthor(int id, AuthorPostDTO authorToUpdate)
        {
            var existingAuthor = await _unitOfWork.AuthorRepository.FindByIdAsync(id);

            if (existingAuthor is null)
            {
                throw new BadHttpRequestException("Author not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(authorToUpdate, existingAuthor);

            _unitOfWork.AuthorRepository.Update(existingAuthor);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var existingAuthor = await _unitOfWork.AuthorRepository.FindByIdAsync(id);

            if (existingAuthor is null)
            {
                throw new BadHttpRequestException("Author not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.AuthorRepository.Remove(existingAuthor);

            await _unitOfWork.CommitAsync();
        }
    }
}
