using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressGetDTO>().ReverseMap();
            CreateMap<Address, AddressPostDTO>().ReverseMap();
        }
    }
}