using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBorrowRepository : IBaseRepository<Borrow> 
    {
        Task<Borrow> AddBorrow(int id);
    }
}