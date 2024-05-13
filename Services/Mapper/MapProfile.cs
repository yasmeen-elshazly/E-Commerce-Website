using AutoMapper;
using DTOs.DTOs.Orders;
using DTOs.DTOs.Category;
using DTOs.DTOs.Products;
using Models.Models;
using DTOs.DTOs.OrderProducts;

namespace Services.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            
            CreateMap<GetAllCategoryDTO,Category>().ReverseMap();
            CreateMap<CreateOrUpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateOrUpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetAllProductsDTO, Product>().ReverseMap();
            CreateMap<GetProductsByCategoryDTO, Product>().ReverseMap();
            CreateMap<GetCreatedorUpdatedProductDTO, Product>().ReverseMap();
            CreateMap<GetAllOrdersDTO, Order>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<UpdateOrderDTO,Order>().ReverseMap();
            CreateMap<GetUpdatedorCreaterOrderDTO, Order>().ReverseMap();
            CreateMap<GetOrCreateOrderProductDTO, OrderProduct>().ReverseMap();
        }
    }
}
