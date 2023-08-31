using AutoMapper;
using Q1.Models;

namespace Q1.Dtos
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Director, DirectDto>().ForMember(x => x.dobString, otp => otp.MapFrom(m => m.Dob.ToString("dd/MM/yyyy")))
                           .ForMember(x => x.gender, otp => otp.MapFrom(m => m.Male == true ? "Male" : "Female"));


            CreateMap<Movie, MovieRM>().ForMember(x => x.releaseYear, otp => otp.MapFrom(m => m.ReleaseDate.Value.ToString("yyyy")))
                .ForMember(x => x.producerName, otp => otp.MapFrom(m => m.Producer.Name))
                .ForMember(X => X.directorName, otp => otp.MapFrom(c => c.Director.FullName))
                .ForMember(x => x.genres, otp => otp.MapFrom(c => c.Genres))
                .ForMember(x => x.stars, otp => otp.MapFrom(c => c.Stars)); CreateMap<Star, StarRM>();
            CreateMap<Genre, GenerRM>();
            CreateMap<Models.Director, DirectRm>().ForMember(x => x.dobString, otp => otp.MapFrom(m => m.Dob.ToString("dd/MM/yyyy")))
                .ForMember(x => x.gender, otp => otp.MapFrom(m => m.Male == true ? "Male" : "Female"))
                .ForMember(x => x.movies, otp => otp.MapFrom(m => m.Movies));
        }
    }
}
