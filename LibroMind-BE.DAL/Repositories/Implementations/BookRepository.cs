using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibroMindContext context) : base(context) { }
    }
}