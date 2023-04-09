using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(LibroMindContext context) : base(context) { }

        public async Task<IEnumerable<Review?>> FindReviewsDetailsAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .ToListAsync();
        }
    }
}