using AutoMapper;
using DirectorInformation.API.Models;
using DirectorInformation.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DirectorInformation.API.Controllers
{
    [Route("api/directors/{directorId}/films")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;

        private readonly IDirectorInformationRepository _directorInfoRepository;
        private readonly IMapper _mapper;

        public FilmController(ILogger<FilmController> logger,
           IDirectorInformationRepository cityInfoRepository,
           IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _directorInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilms(int directorId)
        {
            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            //if(!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            //{
            //    return Forbid();
            //}
            if (!await _directorInfoRepository.DirectorExistsAsync(directorId))
            {
                _logger.LogInformation(
                    $"Director with id {directorId} wasn't found when accesing films.");
                return NotFound();
            }
            var filmsForDirector = await _directorInfoRepository.GetFilmsForDirectorAsync(directorId);

            return Ok(_mapper.Map<IEnumerable<FilmDto>>(filmsForDirector));
        }

        [HttpGet("{filmid}", Name = "GetFilm")]
        public async Task<ActionResult<FilmDto>> GetFilm(
            int directorId, int filmId)
        {
            if (!await _directorInfoRepository.DirectorExistsAsync(directorId))
            {
                return NotFound();
            }

            var film = await _directorInfoRepository
                .GetFilmForDirectorAsync(directorId, filmId);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FilmDto>(film));
        }





        [HttpPost]

        public async Task<ActionResult<FilmDto>> CreateFilm(
            int directorId,
            /*[From Body]*/FilmsForCreationDto film)
        {


            if (!await _directorInfoRepository.DirectorExistsAsync(directorId))
            {
                return NotFound();
            }


            var finalFilm = _mapper.Map<Entities.Film>(film);


            await _directorInfoRepository.AddFilmForDirectorAsync(directorId, finalFilm);
            await _directorInfoRepository.SaveChangesAsync();

            var createdFilmToReturn =
                _mapper.Map<Models.FilmDto>(finalFilm);

            return CreatedAtRoute("GetFilm",
                new
                {
                    directorId = directorId,
                    filmId = createdFilmToReturn.Id
                },
                createdFilmToReturn);
        }


        [HttpPut("{filmid}")]

        public async Task<ActionResult> UpdateFilm(int directorId, int filmId,
            FilmForUpdateDto film)
        {
            if (!await _directorInfoRepository.DirectorExistsAsync(directorId))
            {
                return NotFound();
            }

            var filmEntity = await _directorInfoRepository
                .GetFilmForDirectorAsync(directorId, filmId);
            if (filmEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(film, filmEntity);

            await _directorInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{filmId}")]

        public async Task<ActionResult> DeleteFilm(int directorId, int filmId)
        {

            if (!await _directorInfoRepository.DirectorExistsAsync(directorId))
            {
                return NotFound();
            }

            var filmEntity = await _directorInfoRepository.
                GetFilmForDirectorAsync(directorId, filmId);
            if (filmEntity == null)
            {
                return NotFound();
            }

            _directorInfoRepository.DeleteFilm(filmEntity);
            await _directorInfoRepository.SaveChangesAsync();

            return NoContent();

        }
    }

}
