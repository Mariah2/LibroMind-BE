using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<BookCard>> FindBookCardsAsync();
        Task<IEnumerable<BookCard>> FindBookCardsByUserIdAsync(int id);
        Task<IEnumerable<BookCard>> FindBookCardsForLibraryByParamAsync(int id, string searchParam);
        Task<Book?> FindBookDetailsByIdAsync(int id);
        Task<IEnumerable<Book>> FindLibraryBooksByIdAsync(int id);

    }
}