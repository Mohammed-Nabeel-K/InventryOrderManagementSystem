using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<User,UserDto>()
                .ForMember(d => d.Password,x => x.MapFrom(u => u.PasswordHashed))
                .ReverseMap()
                .ForMember(u => u.PasswordHashed, x => x.MapFrom(d => d.Password));
            CreateMap<OrderDto, Order>();
            CreateMap<Product, ProductGetDto>();

        }
    }
}
