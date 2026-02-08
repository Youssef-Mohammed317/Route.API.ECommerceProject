using AutoMapper;
using E_Commerce.Domian.Entites.OrderModule;
using E_Commerce.Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.MappingProfiles.OrderModule;

public class OrderModuleProfile : Profile
{
    public OrderModuleProfile()
    {
        CreateMap<ShippingAddressDto, ShippingAddress>().ReverseMap();
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

        CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
    }
}
