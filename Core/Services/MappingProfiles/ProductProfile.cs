using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.productBrand.Name))
                            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.productType.Name));
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();

        }
    }
}
