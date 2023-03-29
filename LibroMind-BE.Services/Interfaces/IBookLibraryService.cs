using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookLibraryService
    {
        public Task<IEnumerable<BookLibraryGetDTO>> FindBookLibrariesAsync();
        public Task<BookLibraryGetDTO> FindBookLibraryByIdAsync(int id);
        public Task AddBookLibrary(BookLibraryPostDTO bookLibraryToAdd);
        public Task UpdateBookLibrary(int id, BookLibraryPutDTO bookLibraryToUpdate);
        public Task DeleteBookLibrary(int id);
    }
}
