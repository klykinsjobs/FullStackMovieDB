using FullStackMovieDB.Server.Data;
using FullStackMovieDB.Server.Models;

namespace FullStackMovieDB.Server.Services
{
    public class MovieRepository(MovieDbContext context) : IMovieRepository
    {
        private readonly MovieDbContext _context = context;

        public IEnumerable<Movie> GetAll()
            => [.. _context.Movies.OrderBy(m => m.Id)];

        public Movie? Get(int id)
            => _context.Movies.Find(id);

        public Movie Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public bool Update(int id, Movie movie)
        {
            var existing = _context.Movies.Find(id);
            if (existing == null)
                return false;

            existing.Title = movie.Title;
            existing.IsWatched = movie.IsWatched;
            existing.Rating = movie.Rating;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return true;
        }
    }
}
