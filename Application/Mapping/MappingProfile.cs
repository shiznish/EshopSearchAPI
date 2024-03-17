using Application.Features.Category.Commands;
using Application.Features.Product.Commands;
using Application.Features.Product.Queries;
using Application.Features.User.Queries;
using AutoMapper;
using Domain.Customers;
using Domain.Products;

namespace Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<Product, ProductDetailsDto>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<MoneyDto, Money>().ReverseMap();
        CreateMap<SearchHistoryDto, SearchHistory>().ReverseMap();
    }
}
