using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Movie_Api;
using System.ComponentModel.DataAnnotations;

namespace Movie_App.Model
{
    public class Ticket
    {

        //[BsonId]
        //[BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }
        //public string MovieId { get; set; }

        [Required(ErrorMessage = "Please enter number of tickets you want to book")]
        public int numberOfTickets { get; set; }


        [Required(ErrorMessage = "Enter your seat choice")]
        public List<string> seatNumber { get; set; }

       
        public Movies Movie { get; set; }
    }
    
}
   
