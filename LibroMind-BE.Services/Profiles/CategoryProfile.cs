using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
            CreateMap<Category, CategoryPostDTO>().ReverseMap();
        }
    }
}
