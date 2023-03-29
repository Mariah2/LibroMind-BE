using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Library, LibraryGetDTO>().ReverseMap();
            CreateMap<Library, LibraryPostDTO>().ReverseMap();
            CreateMap<Library, LibraryDetailsGetDTO>()
                .ForMember(dst => dst.Address, src => src.MapFrom(l => l.Address));
        }
    }
}
