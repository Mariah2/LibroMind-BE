using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookGetDTO>> FindBooksAsync();
        public Task<IEnumerable<BookCardGetDTO>> FindBookCardsAsync();
        public Task<IEnumerable<BookCardGetDTO>> FindBookCardsByUserIdAsync(int id);
        public Task<IEnumerable<BookCardGetDTO>> FindBookCardsForLibraryByParamAsync(int id, string searchParam);
        public Task<BookDetailsGetDTO> FindBookDetailsByIdAsync(int id);
        public Task<BookGetDTO> FindBookByIdAsync(int id);
        public Task AddBook(BookPostDTO bookToAdd);
        public Task UpdateBook(int id, BookPostDTO bookToUpdate);
        public Task DeleteBook(int id);
    }
}
