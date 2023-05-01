using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookLibraryRepository : BaseRepository<BookLibrary>, IBookLibraryRepository
    {
        public BookLibraryRepository(LibroMindContext context) : base(context) { }

        public async Task<BookLibrary?> FindBookLibraryByBookIdAndLibraryIdAsync(int bookId, int libraryId)
        {
            return await _context.BookLibraries
                .FirstOrDefaultAsync(bl => bl.BookId == bookId && bl.LibraryId == libraryId);
        }
    }
}