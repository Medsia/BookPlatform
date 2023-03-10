using AutoMapper;
using BookPlatform.BLL.Models;
using BookPlatform.DAL.Entities;

namespace BookPlatform.BLL.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
