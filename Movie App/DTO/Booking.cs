using System.ComponentModel.DataAnnotations;

namespace Movie_App.DTO
{
    public class Booking
    {
        [Required(ErrorMessage = "Please enter number of tickets you want to book")]
        public int numberOfTickets { get; set; }


        [Required(ErrorMessage = "Enter your seat choice")]
        public List<string> seatNumber { get; set; }


    }
}
