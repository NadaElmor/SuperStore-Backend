using AutoMapper;
using Store.Core.Entities;
using SuperStore.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.DTOs;
using IdentityAddress = SuperStore.Core.Entities.User.Address;

namespace SuperStore.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>().ForMember(p=>p.ProductBrand,o=>o.MapFrom(o=>o.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(o => o.ProductType.Name))
                .ForMember(p=>p.PictureUrl,o=>o.MapFrom<ProductPicURLResolver>());

            CreateMap<CustomerBasketDTO, CustomerBasket>();

            CreateMap<BasketItemDto,BasketItem>();

            CreateMap<AddressDto, Address>();

            CreateMap<AddressDto, IdentityAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(O => O.DeliveryMethod, Op => Op.MapFrom(D => D.DeliveryMethod.ShortName))
                .ForMember(O=>O.DeliveryMethodCost,Op=>Op.MapFrom(D=>D.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(O => O.ProductName, Op => Op.MapFrom(D => D.Product.ProductName))
                .ForMember(O=>O.ProductId,Op=>Op.MapFrom(D=>D.Product.ProductId))
                .ForMember(O=>O.PictureUrl,Op=>Op.MapFrom(D=>D.Product.PictureUrl))
                .ForMember(O=>O.PictureUrl,Op=>Op.MapFrom<OrderPicUrlResolver>());
        }
    }
}
