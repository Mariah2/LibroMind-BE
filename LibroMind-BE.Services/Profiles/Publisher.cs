using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherGetDTO>().ReverseMap();
            CreateMap<Publisher, PublisherPostDTO>().ReverseMap();
        }
    }
}
