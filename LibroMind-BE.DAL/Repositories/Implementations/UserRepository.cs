using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibroMindContext context) : base(context) { }
    }
}