using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {
        public BorrowRepository(LibroMindContext context) : base(context) { }

        public async Task<Borrow?> AddBorrow(int id)
        {
            return await _context.Borrows
                .Include(b => b.BookLibrary)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}