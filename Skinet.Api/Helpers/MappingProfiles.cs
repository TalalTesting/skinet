using AutoMapper;
using Skinet.Api.Dtos;
using Skinet.Core.Entities;
using Skinet.Core.Entities.Identity;

namespace Skinet.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.ProductBrand, 
                    opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, 
                    opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,
                    opt => opt.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
