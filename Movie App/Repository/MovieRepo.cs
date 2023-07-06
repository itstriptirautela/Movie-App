using MongoDB.Bson;
using MongoDB.Driver;
using Movie_Api;
using Movie_App.Model;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Movie_App.Repository
{
    public class MovieRepo:IMovieRepo
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Movies> _movieCollection;
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Ticket> _ticketCollection;
        private readonly MongoClient _client;
        public MovieRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(_configuration.GetConnectionString("MovieConnectionString"));
            _movieCollection = _client.GetDatabase("MovieApp").GetCollection<Movies>("Movie");
            _usersCollection = _client.GetDatabase("MovieApp").GetCollection<User>("Users");
            _ticketCollection = _client.GetDatabase("MovieApp").GetCollection<Ticket>("Ticket");
        }
        public void AddMovie(Movies movie)
        {
            movie.Id = ObjectId.GenerateNewId().ToString();
            _movieCollection.InsertOne(movie);
        }
        public List<Movies> GetAllMovies()
        {
           // return _movieCollection.Find(t=>true).ToList();
            
                return _movieCollection.AsQueryable().Select(u => new Movies
                {     
                    Id=u.Id,
                    movieShow = u.movieShow,
                    TotalTicketsAllotted = u.TotalTicketsAllotted,
                    BookedTickets = u.BookedTickets,
                    Status= u.Status,
                  
                }).ToList();

            
        }

        //public List<Movies> GetMovieByMoviename(string movieName)
        //{
        //    var queryExpression = new BsonRegularExpression(new Regex(movieName, RegexOptions.None));
        //    FilterDefinition<Movies> filterDefinition = Builders<Movies>.Filter.Regex("movieName", queryExpression);
        //    return _movieCollection.Find(filterDefinition).ToEnumerable().Select(m => new Movies
        //    {
        //        Id = m.Id,
        //        movieShow = new MovieShow()
        //        {
        //            MovieName = movieName,
                    
        //        },
        //        TotalTicketsAllotted = m.TotalTicketsAllotted,
                
        //    }).ToList();
        //}
        public List<Movies> GetMovieByMoviename(string movieName)
        {
            var data = _movieCollection.Find(m => m.movieShow.MovieName == movieName).ToEnumerable().Select(m => new Movies
            {
                Id = m.Id,
                movieShow = m.movieShow,
                
                TotalTicketsAllotted = m.TotalTicketsAllotted,

            }).ToList();
            return data;

        }
        public async Task<string> DeleteMovie(string movieName)
        {
            try
            {
                var tweet = await _movieCollection.FindOneAndDeleteAsync(t => t.movieShow.MovieName == movieName);
                return "Movie deleted sucessfully";

            }
            catch (Exception e)
            {
                return "Movie not found";
            }
        }





    }
}
