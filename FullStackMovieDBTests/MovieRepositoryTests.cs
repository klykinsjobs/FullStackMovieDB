using FullStackMovieDB.Server.Data;
using FullStackMovieDB.Server.Models;
using FullStackMovieDB.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace FullStackMovieDBTests
{
    public class MovieRepositoryTests
    {
        private MovieDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MovieDbContext(options);
        }

        [Fact]
        public void Add_AddsMovie()
        {
            var context = CreateContext();
            var repo = new MovieRepository(context);

            var movie = new Movie { Title = "Test", IsWatched = false };
            repo.Add(movie);

            Assert.Equal(1, context.Movies.Count());
            Assert.Equal("Test", context.Movies.First().Title);
        }

        [Fact]
        public void GetAll_ReturnsOrderedMovies()
        {
            var context = CreateContext();
            context.Movies.AddRange(
                new Movie { Title = "B" },
                new Movie { Title = "A" }
            );
            context.SaveChanges();

            var repo = new MovieRepository(context);

            var result = repo.GetAll().ToList();

            Assert.Equal(2, result.Count);
            Assert.True(result[0].Id < result[1].Id);
        }

        [Fact]
        public void Update_UpdatesExistingMovie()
        {
            var context = CreateContext();
            var movie = new Movie { Title = "Old" };
            context.Movies.Add(movie);
            context.SaveChanges();

            var repo = new MovieRepository(context);

            var updated = new Movie { Title = "New", IsWatched = true };
            var success = repo.Update(movie.Id, updated);

            Assert.True(success);
            Assert.Equal("New", context.Movies.First().Title);
        }

        [Fact]
        public void Delete_RemovesMovie()
        {
            var context = CreateContext();
            var movie = new Movie { Title = "Delete Me" };
            context.Movies.Add(movie);
            context.SaveChanges();

            var repo = new MovieRepository(context);

            var success = repo.Delete(movie.Id);

            Assert.True(success);
            Assert.Empty(context.Movies);
        }

        [Fact]
        public void Get_ReturnsNull_WhenNotFound()
        {
            var context = CreateContext();
            var repo = new MovieRepository(context);

            var result = repo.Get(999);

            Assert.Null(result);
        }
    }
}
