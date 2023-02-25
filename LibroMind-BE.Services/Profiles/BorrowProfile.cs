using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Profiles
{
    public class BorrowProfile : Profile
    {
        public BorrowProfile()
        {
            CreateMap<Borrow, BorrowGetDTO>().ReverseMap();
            CreateMap<Borrow, BorrowPostDTO>().ReverseMap();
            CreateMap<Borrow, BorrowPutDTO>().ReverseMap();
        }
    }
}
