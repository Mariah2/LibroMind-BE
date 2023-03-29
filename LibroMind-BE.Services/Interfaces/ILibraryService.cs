using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface ILibraryService
    {
        public Task<IEnumerable<LibraryGetDTO>> FindLibrariesAsync();
        public Task<LibraryGetDTO> FindLibraryByIdAsync(int id);
        public Task<IEnumerable<LibraryDetailsGetDTO>> FindLibraryDetailsAsync();
        public Task<IEnumerable<BookDetailsGetDTO>> FindLibraryBooksByIdAsync(int id);
        public Task AddLibrary(LibraryPostDTO libraryToAdd);
        public Task UpdateLibrary(int id, LibraryPostDTO libraryToUpdate);
        public Task DeleteLibrary(int id);
    }
}
