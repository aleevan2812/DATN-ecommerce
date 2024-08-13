using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Core.Dtos.Category;
using Inventory.Core.Dtos.Product;
using Inventory.Core.Entities;

namespace Inventory.Application.Mappers;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();

        CreateMap<ProductCreateDTO, Product>().ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
        //CreateMap<CreateProductCommand, Product>().ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
        CreateMap<CreateProductCommand, Product>();
        //CreateMap<ProductCreateDTO, Product>().ReverseMap();
        CreateMap<ProductUpdateDTO, Product>().ReverseMap();
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.ImageUrls, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryId != null))
            .ForMember(dest => dest.Seller, opt => opt.Condition(src => src.Seller != null)); // Giữ nguyên nếu CategoryId là null

        CreateMap<Product, ProductDTO>().ReverseMap();

        //CreateMap<ProductImage, ProductImageCreateDTO>().ReverseMap();
        //CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
    }
}