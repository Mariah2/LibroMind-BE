using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> FindUserProfileByIdAsync(int id);
    }
    
}