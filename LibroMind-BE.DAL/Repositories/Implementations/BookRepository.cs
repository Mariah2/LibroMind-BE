using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibroMindContext context) : base(context) { }

        public async Task<IEnumerable<BookCard>> FindBookCardsAsync()
        {
            return await _context.Books
                                .Include(b => b.Author)
                                .Select(b => new BookCard()
                                {
                                    Id = b.Id,
                                    Title = b.Title,
                                    CoverUrl = b.CoverUrl,
                                    Rating = b.Rating,
                                    Author = b.Author,
                                })
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

        public async Task<IEnumerable<BookCard>> FindBookCardsByUserIdAsync(int id)
        {
            return await _context.BookUsers
                                .Include(bu => bu.Book)
                                    .ThenInclude(b => b.Author)
                                .Where(bu => bu.UserId == id)
                                .Select(bu => new BookCard()
                                {
                                    Id = bu.Book.Id,
                                    Title = bu.Book.Title,
                                    CoverUrl = bu.Book.CoverUrl,
                                    Rating = bu.Book.Rating,
                                    Author = bu.Book.Author,
                                })
                                .ToListAsync();

        }

        public async Task<IEnumerable<BookCard>> FindBookCardsForLibraryByParamAsync(int id, string searchParam)
        {
            IQueryable<Book> query = _context.Books.Take(0);

            if (!String.IsNullOrEmpty(searchParam))
            {   
                query = _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.BookLibraries)
                    .Where(b => (b.Title
                        .ToLower()
                        .Contains(searchParam
                            .Trim()
                            .ToLower()) || (b.Author.FirstName + b.Author.LastName)
                        .ToLower()
                        .Contains(searchParam
                            .Trim()
                            .ToLower())) && b.BookLibraries.FirstOrDefault(bl => bl.LibraryId == id) == null);
            }

            var bookCards = await query
                .Select(b => new BookCard()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverUrl = b.CoverUrl,
                    Rating = b.Rating,
                    Author = b.Author,
                }).ToListAsync();

            return bookCards;
        }
    }
}