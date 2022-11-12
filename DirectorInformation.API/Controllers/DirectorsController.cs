using AutoMapper;
using DirectorInformation.API.Models;
using DirectorInformation.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorInformation.API.Controllers
{
    [Route("api/directors")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorInformationRepository _directorInfoRepository;
        private readonly IMapper _mapper;
        const int maxDirectorsPageSize = 20;

        public DirectorsController(IDirectorInformationRepository directorInfoRepository,
            IMapper mapper)
        {
            _directorInfoRepository = directorInfoRepository ?? throw new ArgumentNullException(
                nameof(directorInfoRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorWithoutFilmDto>>> GetDirectors(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxDirectorsPageSize)
            {
                pageSize = maxDirectorsPageSize;
            }
            var directorEntities = await _directorInfoRepository
                .GetDirectorsAsync(name, searchQuery, pageNumber, pageSize);

            //Response.Headers.Add("X-PAGINATION", JsonSerializer.

            return Ok(_mapper.Map<IEnumerable<DirectorWithoutFilmDto>>(directorEntities));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirector(
            int id, bool includeFilms = false)
        {
            var director = await _directorInfoRepository.GetDirectorAsync(id, includeFilms);

            if (director == null)
            {
                return NotFound();
            }

            if (includeFilms)
            {
                return Ok(_mapper.Map<DirectorDto>(director));
            }

            return Ok(_mapper.Map<DirectorWithoutFilmDto>(director));
        }

    }
}
