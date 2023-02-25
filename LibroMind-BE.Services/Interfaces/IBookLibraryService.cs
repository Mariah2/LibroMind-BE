﻿using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBookLibraryService
    {
        public Task<IEnumerable<BookLibraryGetDTO>> FindBookLibraryesAsync();
        public Task<BookLibraryGetDTO> FindBookLibraryByIdAsync(int id);
        public Task AddBookLibrary(BookLibraryPostDTO BookLibraryToAdd);
        public Task UpdateBookLibrary(int id, BookLibraryPutDTO BookLibraryToUpdate);
        public Task DeleteBookLibrary(int id);
    }
}