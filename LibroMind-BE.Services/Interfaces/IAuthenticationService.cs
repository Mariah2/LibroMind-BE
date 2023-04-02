using LibroMind_BE.Services.Models.Post;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> LoginAsync(LoginPostDTO loginPost);
        public Task RegisterAsync(RegisterPostDTO registerPost);
    }
}
