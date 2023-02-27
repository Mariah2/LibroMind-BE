using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorGetDTO>> FindAuthoresAsync();
        public Task<AuthorGetDTO> FindAuthorByIdAsync(int id);
        public Task AddAuthor(AuthorPostDTO authorToAdd);
        public Task UpdateAuthor(int id, AuthorPostDTO authorToUpdate);
        public Task DeleteAuthor(int id);
    }
}
