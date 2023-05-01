﻿using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBookLibraryRepository : IBaseRepository<BookLibrary>
    {
        Task<BookLibrary?> FindBookLibraryByBookIdAndLibraryIdAsync(int bookId, int libraryId);
    }
}