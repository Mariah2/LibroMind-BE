using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(LibroMindContext context) : base(context) { }
    }
}