using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibroMindContext context) : base(context) { }

        public async Task<IEnumerable<Book>> FindBooksDetailsAsync()
        {
            return await _context.Books
                                .Include(b => b.Author)
                                .Include(b => b.Publisher)
                                .ToListAsync();
        }

        public async Task<Book?> FindBookDetailsByIdAsync(int id)
        {
            return await _context.Books
                                .Include(b => b.Author)
                                .Include(b => b.Publisher)
                                .Include(b => b.BookLibraries)
                                    .ThenInclude(bl => bl.Library)
                                .Include(b => b.BookCategories)
                                    .ThenInclude(bc => bc.Category)
                                .Include(b => b.Reviews)
                                    .ThenInclude(r => r.User)
                                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> FindLibraryBooksByIdAsync(int id)
        {
            return await _context.BookLibraries
                                .Include(bl => bl.Book)
                                    .ThenInclude(b => b.Author)
                                .Where(bl => bl.LibraryId == id)
                                .Select(bl => bl.Book)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> FindUserBooksByIdAsync(int id)
        {
            return await _context.BookUsers
                                .Include(bu => bu.Book)
                                    .ThenInclude(b => b.Author)
                                .Where(bu => bu.UserId == id)
                                .Select(bu => bu.Book)
                                .ToListAsync();

        }
    }
}