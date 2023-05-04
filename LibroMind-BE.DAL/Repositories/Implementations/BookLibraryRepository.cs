using LibroMind_BE.DAL.Entities;
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

        public async Task<IEnumerable<BookLibraryCard>> FindBookLibraryCardsByLibraryIdAsync(int libraryId)
        {
            return await _context.BookLibraries
                .Include(bl => bl.Book)
                    .ThenInclude(b => b.Author)
                .Include(bl => bl.Library)
                .Where(bl => bl.LibraryId == libraryId)
                .Select(bl => new BookLibraryCard()
                {
                    Id = bl.Id,
                    BookId = bl.BookId,
                    LibraryId = bl.LibraryId,
                    Quantity = bl.Quantity,
                    Book = new BookCard()
                    {
                        Id = bl.Book.Id,
                        CoverUrl = bl.Book.CoverUrl,
                        Title = bl.Book.Title,
                        Rating = bl.Book.Rating,
                        Author = bl.Book.Author,
                    }
                })
                .ToListAsync();
        }
    }
}