using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(LibroMindContext context) : base(context) { }
    }
}