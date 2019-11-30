using AutoMapper;
using ColorShop.Data.Entities;
using ColorShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Data
{
    public class ColorShopMappingProfile : Profile
    {
        public ColorShopMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember( o => o.OrderId, ex => ex.MapFrom( o =>o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
