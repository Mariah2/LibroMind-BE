using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserGetDTO>> FindUseresAsync();
        public Task<UserGetDTO> FindUserByIdAsync(int id);
        public Task AddUser(UserPostDTO UserToAdd);
        public Task UpdateUser(int id, UserPostDTO UserToUpdate);
        public Task DeleteUser(int id);
    }
}
