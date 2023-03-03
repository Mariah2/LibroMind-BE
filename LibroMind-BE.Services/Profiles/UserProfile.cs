using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;
using LibroMind_BE.Services.Models.Post;

namespace LibroMind_BE.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDTO>().ReverseMap();
            CreateMap<User, UserPostDTO>().ReverseMap();
            CreateMap<User, RegisterPostDTO>().ReverseMap();

        }
    }
}
