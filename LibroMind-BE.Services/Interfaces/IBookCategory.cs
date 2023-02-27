using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookCategoryService
    {
        public Task<IEnumerable<BookCategoryGetDTO>> FindBookCategoryesAsync();
        public Task<BookCategoryGetDTO> FindBookCategoryByIdAsync(int id);
        public Task AddBookCategory(BookCategoryPostDTO bookCategoryToAdd);
        public Task UpdateBookCategory(int id, BookCategoryPostDTO bookCategoryToUpdate);
        public Task DeleteBookCategory(int id);
    }
}
