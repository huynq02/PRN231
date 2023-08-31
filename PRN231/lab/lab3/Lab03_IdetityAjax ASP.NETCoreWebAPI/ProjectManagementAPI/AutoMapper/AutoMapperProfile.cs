using AutoMapper;
using BusinessObjects;
using ProjectManagementAPI.DTO;

namespace ProjectManagementAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductAddRequest>().ReverseMap();
            CreateMap<Product,ProductResponse>().ReverseMap();
        }
    }
}
