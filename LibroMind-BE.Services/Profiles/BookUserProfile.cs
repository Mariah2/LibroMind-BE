using AutoMapper;
using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class BookUserProfile : Profile
    {
        public BookUserProfile()
        {
            CreateMap<BookUser, BookUserGetDTO>().ReverseMap();
            CreateMap<BookUser, BookUserPostDTO>().ReverseMap();
            CreateMap<BookUserCard, BookUserCardGetDTO>().ReverseMap();
        }
    }
}
