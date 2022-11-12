using AutoMapper;

namespace DirectorInformation.API.Profiles
{
    public class FilmProfile : Profile
    {
        public FilmProfile()
        {
            CreateMap<Entities.Film, Models.FilmDto>();
            CreateMap<Models.FilmsForCreationDto, Entities.Film>();
            CreateMap<Models.FilmForUpdateDto, Entities.Film>();
            CreateMap<Entities.Film, Models.FilmForUpdateDto>();
        }
    }
}
