using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookGetDTO>> FindBookesAsync();
        public Task<BookGetDTO> FindBookByIdAsync(int id);
        public Task AddBook(BookPostDTO BookToAdd);
        public Task UpdateBook(int id, BookPostDTO BookToUpdate);
        public Task DeleteBook(int id);
    }
}
