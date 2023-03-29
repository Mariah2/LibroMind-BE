using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(LibroMindContext context) : base(context) { }

        public async Task<IEnumerable<Library>> FindLibraryDetailsAsync()
        {
            return await _context.Libraries
                                .Include(l => l.Address)
                                .ToListAsync();
        }
    }
}