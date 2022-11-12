using DirectorInformation.API.Models;

namespace DirectorInformation.API.Models
{
    public class DirectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfFilms
        {
            get
            {
                return Films.Count;
            }
        }

        public ICollection<FilmDto> Films { get; set; } = new List<FilmDto>();
    }
}
