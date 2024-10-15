using AutoMapper;
using Store.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.DTOs;

namespace SuperStore.Helper
{
    public class OrderPicUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["APIBaseURL"]}/{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }

}

