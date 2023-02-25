using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibroMindContext context) : base(context) { }
    }
}