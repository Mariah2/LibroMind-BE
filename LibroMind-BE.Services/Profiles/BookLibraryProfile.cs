using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class BookLibraryProfile : Profile
    {
        public BookLibraryProfile()
        {
            CreateMap<BookLibrary, BookLibraryGetDTO>().ReverseMap();
            CreateMap<BookLibrary, BookLibraryPostDTO>().ReverseMap();
            CreateMap<BookLibrary, BookLibraryPutDTO>().ReverseMap();
            CreateMap<BookLibrary, BookLibraryDetailsGetDTO>()
                .ForMember(dst => dst.Author, src => src.MapFrom(bl => bl.Book.Author))
                .ForMember(dst => dst.Book, src => src.MapFrom(bl => bl.Book));
        }
    }
}
