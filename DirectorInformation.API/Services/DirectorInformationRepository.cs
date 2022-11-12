using DirectorInformation.API.DbContexts;
using DirectorInformation.API.Entities;
using DirectorInformation.API.Services;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace DirectorInformation.API.Services
{
    public class DirectorInformationRepository : IDirectorInformationRepository
    {
        private readonly DirectorInformationContext _context;

        public DirectorInformationRepository(DirectorInformationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Director>> GetDirectorsAsync()
        {
            return await _context.Directors.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> DirectorNameMatchesDirectorId(string? directorName, int directorId)
        {
            return await _context.Directors.AnyAsync(c => c.Id == directorId && c.Name == directorName);
        }

        public async Task<IEnumerable<Director>> GetDirectorsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Directors as IQueryable<Director>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            return await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToListAsync();
        }

        public async Task<Director?> GetDirectorAsync(
            int directorId, bool includeFilms)
        {
            if (includeFilms)
            {
                return await _context.Directors.Include(c => c.Films)
                    .Where(c => c.Id == directorId).FirstOrDefaultAsync();
            }
            return await _context.Directors.Where(c => c.Id == directorId).FirstOrDefaultAsync();
        }

        public async Task<bool> DirectorExistsAsync(int directorId)
        {
            return await _context.Directors.AnyAsync(c => c.Id == directorId);
        }

        public async Task<Film?> GetFilmForDirectorAsync(
            int directorId, int filmId)
        {
            return await _context.Films
                .Where(p => p.DirectorId == directorId && p.Id == filmId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Film>> GetFilmsForDirectorAsync(int directorId)
        {
            return await _context.Films
                .Where(p => p.DirectorId == directorId)
                .ToListAsync();
        }

        public async Task AddFilmForDirectorAsync(int directorId, Film film)
        {
            var director = await GetDirectorAsync(directorId, false);
            if (director != null)
            {
                director.Films.Add(film);
            }
        }

        public void DeleteFilm(Film film)
        {
            _context.Films.Remove(film);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
