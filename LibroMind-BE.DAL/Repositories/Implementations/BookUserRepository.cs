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
    }
}