using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorGetDTO>().ReverseMap();
            CreateMap<Author, AuthorPostDTO>().ReverseMap();
        }
    }
}