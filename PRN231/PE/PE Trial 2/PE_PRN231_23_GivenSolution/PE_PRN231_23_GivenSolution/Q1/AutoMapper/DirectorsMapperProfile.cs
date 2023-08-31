using AutoMapper;
using Q1.Models;

namespace Q1.AutoMapper
{
    public class DirectorsMapperProfile : Profile
    {
        public DirectorsMapperProfile()
        {
            CreateMap<Director, DTO.DirectorsDTO>()
                .ForMember(o => o.dobString, otp => otp.MapFrom(src => src.Dob.ToString("dd/mm/yyyy")));       
        }
    }
}
