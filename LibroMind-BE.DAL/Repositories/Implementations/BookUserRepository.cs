using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookUserRepository : BaseRepository<BookUser>, IBookUserRepository
    {
        public BookUserRepository(LibroMindContext context) : base(context) { }
    }
}