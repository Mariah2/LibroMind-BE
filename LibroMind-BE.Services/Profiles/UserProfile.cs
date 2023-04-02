using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;
using LibroMind_BE.Services.Models.Get;
using LibroMind_BE.Services.Models.Post;
using LibroMind_BE.Services.Models.Put;

namespace LibroMind_BE.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDTO>().ReverseMap();
            CreateMap<User, UserPostDTO>().ReverseMap();
            CreateMap<User, UserPutDTO>().ReverseMap();
            CreateMap<User, RegisterPostDTO>().ReverseMap();
            CreateMap<User, UserProfileGetDTO>().ReverseMap();
        }
    }
}
