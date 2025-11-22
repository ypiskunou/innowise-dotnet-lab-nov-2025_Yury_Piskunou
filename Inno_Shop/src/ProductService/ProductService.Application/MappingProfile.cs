using AutoMapper;
using ProductService.Entities.Models;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<ProductForCreationDto, Product>();

        CreateMap<ProductForUpdateDto, Product>();
    }
}