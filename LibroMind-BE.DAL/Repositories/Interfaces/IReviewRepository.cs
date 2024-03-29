﻿using LibroMind_BE.DAL.Models;


namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    { 
        Task<IEnumerable<Review?>> FindReviewsDetailsAsync();
    }

    
}
