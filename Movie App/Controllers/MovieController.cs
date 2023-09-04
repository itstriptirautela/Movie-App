using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.WebApi.Controllers;
using Movie_App.Model;
using Movie_App.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
        [HttpGet("AllMovie")]
        public ActionResult<List<Movies>> GetAllMovie()
        {
            try
            {
                _logger.LogInformation("Getting All movies");
                var movieList = _movieService.GetAllMovies();
                if (movieList.Count < 1)
                {
                    _logger.LogWarning("No movie in db");
                    return BadRequest("No movie found");
                }
                _logger.LogInformation("Movie found");
                return Ok(movieList);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddMovie")]
       
        public IActionResult AddMovie([FromBody] Movies movie) //userobj coming from ui
        {

            try 
            {
                if (movie != null)
                {
                    movie.Id = null;
                  _movieService.AddMovie(movie);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Movie Added Successfully"
                    });
                }
               return BadRequest();

            }
            catch (Exception ex)
            {
                
                    _logger.LogTrace(ex.StackTrace);
                    return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        [HttpGet("SearchMovie")]
        public ActionResult<List<Movies>> GetMovieByMovieName(string movieName)
        {
            try
            {
                var movieList = _movieService.GetMovieByMoviename(movieName);
                if (movieList.Count() == 0)
                {
                    return BadRequest(new { Response = "Movie Not Found" });
                }
                return Ok(movieList);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);

                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }

        //[Authorize(Roles = "Admin")]

        [HttpDelete("delete")]
        
      
        public async Task<ActionResult<string>> DeleteMovie(string movieName)
        {
            try
            {
                var response = await _movieService.DeleteMovie(movieName);
                if (response == null)
                {
                    return BadRequest("Movie not found");
                }
                return Ok(new { message = response });
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }


    }
}