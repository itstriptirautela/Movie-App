using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Movie_App.Model
{
    public class User
    {

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "LoginId Required")]
        public string LoginId { get; set; }
      [Required(ErrorMessage = "FirstName Required")]
        public string firstName { get; set; }
       [Required(ErrorMessage = "LastName Required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
         public string email { get; set; }
        [Required(ErrorMessage = "Password Required")]
         public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        public string confirmPassword { get; set; }

        [Required(ErrorMessage = "Contact Number")]
        [Phone]
        public string contactNumber { get; set; }

        [Required(ErrorMessage = "role")]
      
        public string Roles { get; set; }

    }
}

   
