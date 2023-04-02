using LibroMind_BE.Services.Models;
using LibroMind_BE.Services.Models.Get;
using LibroMind_BE.Services.Models.Put;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserGetDTO>> FindUsersAsync();
        public Task<UserGetDTO> FindUserByIdAsync(int id);
        public Task<IEnumerable<BookDetailsGetDTO>> FindUserBooksByIdAsync(int id);
        public Task UpdateUser(int id, UserPutDTO userToUpdate);
        public Task DeleteUser(int id);
        public Task<UserProfileGetDTO> FindUserProfileByIdAsync(int id);
    }
}
