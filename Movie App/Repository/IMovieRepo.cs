using Movie_Api;
using Movie_App.Model;

namespace Movie_App.Repository
{
    public interface IMovieRepo
    {
        public void AddMovie(Movies movie);
        public List<Movies> GetAllMovies();
        public List<Movies> GetMovieByMoviename(string movieName);
        //Task<string> DeleteMovie(string movieName);
        Task<string> DeleteMovie(string movieName);
    }

}
