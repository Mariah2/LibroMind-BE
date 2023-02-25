using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {
        public BorrowRepository(LibroMindContext context) : base(context) { }
    }
}