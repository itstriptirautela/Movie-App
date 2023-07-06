using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Movie_App.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public ObjectId userID { get; set; }
        [Required]
        public string LoginId { get; set; }
        public string token { get; set; }
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [Phone]
        public string contactNumber { get; set; }
     
        public string role { get; set; }
    }
}
