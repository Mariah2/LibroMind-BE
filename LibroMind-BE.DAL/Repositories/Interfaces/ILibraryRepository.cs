﻿using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface ILibraryRepository : IBaseRepository<Library> 
    {
        Task<IEnumerable<Library>> FindLibraryDetailsAsync();

    }
}