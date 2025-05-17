using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;

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

            CreateMap<OrderDto, Order>().ForMember(o => o.Items, x => x.MapFrom(od => od.Items));
            CreateMap<OrderItem, SaleData>()
                .ForMember(s => s.ProductName, x => x.MapFrom(o => o.Product.Name))
                .ForMember(s => s.OrderId, x => x.MapFrom(o => o.Order.Id))
                .ForMember(s => s.OrderItemId, x => x.MapFrom(o => o.Id))
                .ForMember(s => s.QuantitySold,x => x.MapFrom(o =>o.Quantity))
                .ForMember(s => s.SaleDate,x => x.MapFrom(o => o.Order.OrderDate));

            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, InventryDto>()
                .ForMember(i => i.level, x => x.MapFrom(p => p.QuantityInStock <= p.ReorderLevel? "low":"high"));

            CreateMap<StockMovementDto, StockMovement>();

        }
    }
}
