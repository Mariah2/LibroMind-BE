using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface ILibraryService
    {
        public Task<IEnumerable<LibraryGetDTO>> FindLibraryesAsync();
        public Task<LibraryGetDTO> FindLibraryByIdAsync(int id);
        public Task AddLibrary(LibraryPostDTO LibraryToAdd);
        public Task UpdateLibrary(int id, LibraryPostDTO LibraryToUpdate);
        public Task DeleteLibrary(int id);
    }
}
