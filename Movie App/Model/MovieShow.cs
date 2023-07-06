using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movie_App.Model
{
    public class MovieShow
    {
  
        public string MovieName { get; set; }

        public string TheatreName { get; set; }
    }
}
