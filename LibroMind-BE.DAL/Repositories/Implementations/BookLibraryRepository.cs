using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookLibraryRepository : BaseRepository<BookLibrary>, IBookLibraryRepository
    {
        public BookLibraryRepository(LibroMindContext context) : base(context) { }
    }
}