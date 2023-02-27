using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryGetDTO>> FindCategoryesAsync();
        public Task<CategoryGetDTO> FindCategoryByIdAsync(int id);
        public Task AddCategory(CategoryPostDTO categoryToAdd);
        public Task UpdateCategory(int id, CategoryPostDTO categoryToUpdate);
        public Task DeleteCategory(int id);
    }
}
