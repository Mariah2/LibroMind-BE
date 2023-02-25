using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibroMindContext context) : base(context) { }
    }
}