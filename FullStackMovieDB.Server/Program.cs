using FullStackMovieDB.Server.Data;
using FullStackMovieDB.Server.Models;
using FullStackMovieDB.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace FullStackMovieDB.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlite("Data Source=movies.db"));
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
                if (!db.Movies.Any())
                {
                    db.Movies.AddRange(
                        new Movie { Title = "Test Movie 1", IsWatched = true, Rating = 10 },
                        new Movie { Title = "Test Movie 2", IsWatched = false, Rating = 1 },
                        new Movie { Title = "Test Movie 3", IsWatched = false, Rating = null }
                    );
                    db.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
