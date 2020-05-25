using CorsAspNetCoreWebApi.Data;
using CorsAspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsAspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private MoviesDbContext _db;

        public MoviesController(MoviesDbContext db)
        {
            _db = db;
        }

        [EnableCors(Constants.AllowSpecificOriginsCorsPolicy)]
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return _db.Movies.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            return _db.Movies.SingleOrDefault(d => d.Id.Equals(id));
        }

        public async Task<IActionResult> Post([FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieEntity = await _db.Movies.FindAsync(movie.Id);

            if (movieEntity != null)
                return Conflict();

            _db.Movies.Add(movie);

            await _db.SaveChangesAsync();

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{movie.Id}", UriKind.Absolute), movie);
        }
    }
}
