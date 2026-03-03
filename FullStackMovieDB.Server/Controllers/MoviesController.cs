using FullStackMovieDB.Server.Models;
using FullStackMovieDB.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStackMovieDB.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController(IMovieRepository repository) : ControllerBase
    {
        private readonly IMovieRepository _repository = repository;

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Movie> Get(int id)
        {
            var movie = _repository.Get(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<Movie> Create([FromBody] Movie movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title))
                return BadRequest("Title is required.");

            movie.Id = 0;
            var created = _repository.Add(movie);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Movie movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title))
                return BadRequest("Title is required.");

            var success = _repository.Update(id, movie);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var success = _repository.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
