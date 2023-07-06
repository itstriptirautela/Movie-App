using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Movie_Api;
using Movie_App.DTO;
using Movie_App.Model;

namespace Movie_App.Repository
{
    public class TicketRepo : ITicketRepo
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Movies> _movieCollection;
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Ticket> _ticketCollection;
        private readonly MongoClient _client;
        public TicketRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(_configuration.GetConnectionString("MovieConnectionString"));
            _movieCollection = _client.GetDatabase("MovieApp").GetCollection<Movies>("Movie");
            _usersCollection = _client.GetDatabase("MovieApp").GetCollection<User>("Users");
            _ticketCollection = _client.GetDatabase("MovieApp").GetCollection<Ticket>("Ticket");
        }


        //public void BookTicket(Ticket ticket, string movieName, string TheaterName)
        //{
        //    Movies movie = _movieCollection.Find(x => x.movieShow.MovieName == movieName && x.movieShow.TheatreName == TheaterName).FirstOrDefault();
        //    if (movie != null)
        //    {
        //        ticket.Movie = movie;
        //        _ticketCollection.InsertOne(ticket);



        //    }
        //}
        public void BookTicket(Ticket ticket, string movieName, string TheaterName) { 


                Movies movie = _movieCollection.Find(x => x.movieShow.MovieName == movieName).FirstOrDefault();


                if (movie == null)

                {

                    throw new Exception($"Movie with name '{movieName}' does not exist in the database.");

                }


                if (!movie.movieShow.TheatreName.Contains(TheaterName))

                {

                    throw new Exception($"Theater with name '{TheaterName}' does not exist for the movie '{movieName}'.");

                }


                bool seatAlreadyBooked = _ticketCollection.Find(x => x.Movie.movieShow.MovieName == movieName &&

                                                                      x.Movie.movieShow.TheatreName == TheaterName &&

                                                                      x.seatNumber.Any(s => ticket.seatNumber.Contains(s))).Any();


                if (seatAlreadyBooked)

                {

                    throw new Exception("One or more of the specified seat numbers are already booked for the given movie and theater.");

                }
            
            

            ticket.Movie = movie;

                _ticketCollection.InsertOne(ticket);

            }
        public int GetBookedTicketsCount(string movieName, string theatreName)
        {
            var filter = Builders<Movies>.Filter.Eq(movie => movie.movieShow.MovieName, movieName) &
            Builders<Movies>.Filter.Eq(movie => movie.movieShow.TheatreName, theatreName);
            var projection = Builders<Movies>.Projection.Include(movie => movie.BookedTickets);
            var result =_movieCollection.Find(filter).Project<Movies>(projection).FirstOrDefault();

         
           if (result != null)
            {
                return result.BookedTickets;
            }
           return 0;
        }
        public void UpdateBookedTickets(string movieName, string theatreName,int numberOfTicket)
        {
            var filter = Builders<Movies>.Filter.And(Builders<Movies>.Filter.Eq(movie => movie.movieShow.MovieName, movieName) ,
            Builders<Movies>.Filter.Eq(movie => movie.movieShow.TheatreName, theatreName));

            var update = Builders<Movies>.Update.Inc(movie => movie.BookedTickets,numberOfTicket);
           
         _movieCollection.UpdateOne(filter,update);
         }
        public void UpdateTicketStatus(string movieName, string theatreName, string ticketStatus)
        {
            var filter = Builders<Movies>.Filter.And (Builders<Movies>.Filter.Eq(movie => movie.movieShow.MovieName, movieName) ,
            Builders<Movies>.Filter.Eq(movie => movie.movieShow.TheatreName, theatreName));

           //var movie = _movieCollection.Find(filter).FirstOrDefault();
           // if(movie != null)
           // {
           //     if (movie.TotalTicketsAllotted == 0)
           //         movie.Status = "SOLD OUT";
           //     else if(movie.BookedTickets < movie.TotalTicketsAllotted)
           //         movie.Status = "BOOK ASAP";

                var update = Builders<Movies>.Update.Set(movies => movies.Status, ticketStatus);
                _movieCollection.UpdateOne(filter, update);
            

            
        }

    }

    }

