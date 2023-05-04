using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookUserService
    {
        public Task<IEnumerable<BookUserGetDTO>> FindBookUsersAsync();
        public Task<IEnumerable<BookUserCardGetDTO>> FindBookUserCardsByUserIdAsync(int userId);
        public Task<BookUserGetDTO> FindBookUserByIdAsync(int id);
        public Task AddBookUser(BookUserPostDTO bookUserToAdd);
        public Task UpdateBookUser(int id, BookUserPostDTO bookUserToUpdate);
        public Task DeleteBookUser(int id);
    }
}
