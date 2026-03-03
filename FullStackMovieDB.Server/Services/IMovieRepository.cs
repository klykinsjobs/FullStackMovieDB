using FullStackMovieDB.Server.Models;

namespace FullStackMovieDB.Server.Services
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie? Get(int id);
        Movie Add(Movie movie);
        bool Update(int id, Movie movie);
        bool Delete(int id);
    }
}
