using AutoMapper;

namespace DirectorInformation.API.Profiles
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<Entities.Director, Models.DirectorWithoutFilmDto>();
            CreateMap<Entities.Director, Models.DirectorDto>();
        }
    }
}
