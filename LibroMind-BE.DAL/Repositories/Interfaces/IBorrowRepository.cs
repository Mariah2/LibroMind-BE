using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBorrowRepository : IBaseRepository<Borrow> 
    {
        Task<IEnumerable<BorrowingDetails>> FindBorrowingsByLibraryIdAsync(int libraryId);
        Task<IEnumerable<BorrowingDetails>> FindBorrowingsByLibraryIdAndParamAsync(int libraryId, string? searchParam);
        Task<IEnumerable<BorrowingDetails>> FindBorrowingsByUserIdAsync(int userId);
    }
}