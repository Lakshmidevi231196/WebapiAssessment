using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmDBContext filmdbcontext;
        public FilmController(FilmDBContext filmDbContext)
        {
            filmdbcontext = filmDbContext;
        }



        [HttpGet]
        public IEnumerable<Film> GetMovies()
        {
            return filmdbcontext.film.ToList();
        }
        [HttpGet("GetMovieById")]
        public Film GetMovieById(int Id)
        {
            return filmdbcontext.film.Find(Id);
        }
        [HttpPost("InsertMovie")]
        public IActionResult InsertMovie([FromBody] Film film)
        {
            if (film.Id.ToString() != "")
            {

                filmdbcontext.film.Add(film);
                filmdbcontext.SaveChanges();
                return Ok("Film details saved successfully");
            }
            else
                return BadRequest();
        }
       
[HttpPut("UpdateTutorial")]
public IActionResult UpdateFilm([FromBody] Film film)
        {
            if (film.Id.ToString() != "")
            {
                filmdbcontext.Entry(film).State = EntityState.Modified;
                filmdbcontext.SaveChanges();
                return Ok("Film details has been Updated successfully");
            }
            else
                return BadRequest();
        }
        [HttpDelete("DeleteTutorial")]
        public IActionResult DeleteTutorial(int filmId)
        {
            var result = filmdbcontext.film.Find(filmId);
            filmdbcontext.film.Remove(result);
            filmdbcontext.SaveChanges();
            return Ok("Film details has been successfully deleted");
        }

    }
}
