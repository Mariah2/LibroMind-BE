using AutoMapper;
using LibroMind_BE.DAL.Entities;
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
            CreateMap<BookCard, BookCardGetDTO>().ReverseMap();
            CreateMap<Book, BookDetailsGetDTO>()
                .ForMember(dst => dst.Author, src => src.MapFrom(b => b.Author))
                .ForMember(dst => dst.Publisher, src => src.MapFrom(b => b.Publisher))
                .ForMember(dst => dst.Reviews, src => src.MapFrom(b => b.Reviews))
                .ForMember(dst => dst.BookCategories, src => src.MapFrom(b => b.BookCategories))
                .ForMember(dst => dst.BookLibraries, src => src.MapFrom(b => b.BookLibraries));
        }
    }
}
