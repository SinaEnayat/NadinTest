using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;

namespace ProductManagement.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
} 