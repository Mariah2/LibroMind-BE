using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BookUserRepository : BaseRepository<BookUser>, IBookUserRepository
    {
        public BookUserRepository(LibroMindContext context) : base(context) { }

        public async Task<bool> CheckIfBookAlreadyInToRead(int userId, int bookId)
        {
            return await _context.BookUsers.FirstOrDefaultAsync(
                bu => bu.UserId == userId && bu.BookId == bookId) is not null;
        }

        public async Task<IEnumerable<BookUserCard>> FindBookUserCardsByUserIdAsync(int userId)
        {
            return await _context.BookUsers
                                .Include(bu => bu.Book)
                                    .ThenInclude(b => b.Author)
                                .Include(bu => bu.User)
                                .Where(bu => bu.UserId == userId)
                                .Select(bu => new BookUserCard()
                                {
                                    Id = bu.Id,
                                    BookId = bu.BookId,
                                    UserId = bu.UserId,
                                    Book = new BookCard()
                                    {
                                        Id = bu.Book.Id,
                                        Title = bu.Book.Title,
                                        CoverUrl = bu.Book.CoverUrl,
                                        Rating = bu.Book.Rating,
                                        Author = bu.Book.Author,
                                    }
                                })
                                .ToListAsync();

        }
    }
}