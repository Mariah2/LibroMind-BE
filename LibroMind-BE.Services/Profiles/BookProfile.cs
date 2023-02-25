using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookGetDTO>().ReverseMap();
            CreateMap<Book, BookPostDTO>().ReverseMap();
        }
    }
}
