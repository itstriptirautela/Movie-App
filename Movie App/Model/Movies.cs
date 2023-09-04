using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Movie_App.Model;
using System.ComponentModel.DataAnnotations;

namespace Movie_App.Model
{
    public class Movies
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]   
        public string Id { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        public MovieShow movieShow { get; set; }
       
        [Required(ErrorMessage = "Please enter total number of tickets available")]
        public int TotalTicketsAllotted { get; set; }

        public int BookedTickets { get;set; }

        public string Status { get; set; }
        public int ticketPrice { get; set; }

    }
}
