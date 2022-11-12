using DirectorInformation.API.Entities;

namespace DirectorInformation.API.Services
{
    public interface IDirectorInformationRepository
    {
        Task<IEnumerable<Director>> GetDirectorsAsync();
        Task<IEnumerable<Director>> GetDirectorsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Director?> GetDirectorAsync(int directorId, bool includeFilms);
        Task<bool> DirectorExistsAsync(int directorId);

        Task<IEnumerable<Film>> GetFilmsForDirectorAsync(int directorId);
        Task<Film?> GetFilmForDirectorAsync(int directorId, int filmId);

        Task AddFilmForDirectorAsync(int directorId, Film film);

        void DeleteFilm(Film film);

        Task<bool> DirectorNameMatchesDirectorId(string? cityName, int cityId);
        Task<bool> SaveChangesAsync();

    }
}
