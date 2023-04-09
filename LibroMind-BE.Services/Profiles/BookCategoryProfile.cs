using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class BookCategoryProfile : Profile
    {
        public BookCategoryProfile()
        {
            CreateMap<BookCategory, BookCategoryGetDTO>().ReverseMap();
            CreateMap<BookCategory, BookCategoryPostDTO>().ReverseMap();
            CreateMap<BookCategory, BookCategoryDetailsGetDTO>().ReverseMap();
        }
    }
}
