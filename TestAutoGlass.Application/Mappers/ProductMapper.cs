using AutoMapper;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Requests.Create;
using TestAutoGlass.Domain.Requests.Update;
using TestAutoGlass.Domain.Responses;

namespace TestAutoGlass.Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<CreateProductRequest, Products>();
            CreateMap<UpdateProductRequest, Products>();

            CreateMap<Products, ProductsResponse>();

        }
    }
}
