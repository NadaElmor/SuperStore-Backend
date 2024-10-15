using AutoMapper;
using AutoMapper.Execution;
using Store.Core.Entities;
using SuperStore.DTOs;

namespace SuperStore.Helper
{
    public class ProductPicURLResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["APIBaseURL"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
