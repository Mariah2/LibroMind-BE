using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookCategoryService
    {
        public Task<IEnumerable<BookCategoryGetDTO>> FindBookCategoryesAsync();
        public Task<BookCategoryGetDTO> FindBookCategoryByIdAsync(int id);
        public Task AddBookCategory(BookCategoryPostDTO BookCategoryToAdd);
        public Task UpdateBookCategory(int id, BookCategoryPostDTO BookCategoryToUpdate);
        public Task DeleteBookCategory(int id);
    }
}
