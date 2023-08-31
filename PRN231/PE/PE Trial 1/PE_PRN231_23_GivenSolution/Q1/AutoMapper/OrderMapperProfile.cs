
using AutoMapper;
using Q1.Models;

namespace Q1.AutoMapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile() 
        {
            CreateMap<Order, DTO.OrderDTO>()
                .ForMember(o => o.customerName, otp => otp.MapFrom(src => src.Customer!.CompanyName))
                .ForMember(o => o.employeeName, otp => otp.MapFrom(o => o.Employee!.FirstName + ' ' + o.Employee!.LastName))
                .ForMember(o => o.employeeDepartmentId, otp => otp.MapFrom(o => o.Employee!.EmployeeId))
                .ForMember(o => o.employeeDepartmentName, otp => otp.MapFrom(o => o.Employee!.Department!.DepartmentName));
        }
    }
}
