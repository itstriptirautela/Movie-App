using Movie_Api;
using Movie_App.Model;
using Movie_App.Repository;

namespace Movie_App.Service
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepo _movieRepository;

        public MovieService(IMovieRepo movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public void AddMovie(Movies movie)
        {
            _movieRepository.AddMovie(movie);
        }
        public List<Movies> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }
        public List<Movies> GetMovieByMoviename(string movieName)
        {
            return _movieRepository.GetMovieByMoviename(movieName);
        }
        public async Task<string> DeleteMovie(string username)
        {
            
            return await _movieRepository.DeleteMovie(username);
        }
    }
}
