using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBookUserRepository : IBaseRepository<BookUser>
    {
        public Task<bool> CheckIfBookAlreadyInToRead(int userId, int bookId);
    }
}