using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> FindBooksDetailsAsync();
        Task<Book?> FindBookDetailsByIdAsync(int id);
        Task<IEnumerable<Book>> FindLibraryBooksByIdAsync(int id);
        Task<IEnumerable<Book>> FindUserBooksByIdAsync(int id);

    }
}