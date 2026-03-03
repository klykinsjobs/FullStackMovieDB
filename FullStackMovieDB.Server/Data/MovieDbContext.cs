using FullStackMovieDB.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackMovieDB.Server.Data
{
    public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies => Set<Movie>();
    }
}
