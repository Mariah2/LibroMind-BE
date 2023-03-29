using LibroMind_BE.Services.Models;
using LibroMind_BE.Services.Models.Put;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserGetDTO>> FindUseresAsync();
        public Task<UserGetDTO> FindUserByIdAsync(int id);
        public Task UpdateUser(int id, UserPutDTO userToUpdate);
        public Task DeleteUser(int id);
    }
}
