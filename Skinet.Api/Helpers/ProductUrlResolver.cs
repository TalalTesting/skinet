using AutoMapper;
using Microsoft.Extensions.Configuration;
using Skinet.Api.Dtos;
using Skinet.Core.Entities;

namespace Skinet.Api.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return _config["ApiUrl"] + source.PictureUrl;

            return null;
        }
    }
}
