using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Api.Controllers;
using Movie_App.DTO;
using Movie_App.Model;
using Movie_App.Service;

namespace Movie_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService, IMovieService movieService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _movieService = movieService;
    }
        [HttpPost]
        //public IActionResult BookTicket([FromBody] Ticket ticket)
        //{
        //    Ticket tickets = new Ticket
        //    {
        //        numberOfTickets = ticket.numberOfTickets,
        //        seatNumber = ticket.seatNumber,

        //    };
        //    _ticketService.BookTicket(ticket);
        //    return Ok("Ticket Ticket Booked ");
        //}
        public IActionResult BookTicket(Booking ticket, string movieName,  string theaterName)
        {
            int numberOfTickets = ticket.numberOfTickets;
            List<string> seatNumber = ticket.seatNumber;
            Ticket tickets = new Ticket
            {
                numberOfTickets = numberOfTickets,
                seatNumber = seatNumber
            };

            List<Movies> movies = _movieService.GetAllMovies();
            Movies movie = movies.FirstOrDefault(m=>m.movieShow.MovieName == movieName && m.movieShow.TheatreName == theaterName);
            if (movie != null)
            {
                int totalticketAlloted = movie.TotalTicketsAllotted;
                int bookedTicketCount = _ticketService.GetBookedTicketsCount(movieName, theaterName);
                int availableTickets = totalticketAlloted - bookedTicketCount;

                if (availableTickets >= numberOfTickets)
                {

                    _ticketService.BookTicket(movieName, theaterName, tickets);
                    _ticketService.UpdateBookedTickets(movieName, theaterName, numberOfTickets);

                    if (availableTickets - numberOfTickets == 0 )
                    {
                        // movie.Status = "SOLD OUT";
                        _ticketService.UpdateTicketStatus(movieName, theaterName, "SOLD OUT");
                    }
                    else if(availableTickets - numberOfTickets <20)
                    {
                        _ticketService.UpdateTicketStatus(movieName, theaterName, "BOOK ASAP");
                    }
                    return Ok(" Ticket booked Succesfully");
                }
                else
                {
                    return BadRequest("Not enough Ticket avaiable");
                }
            }
            else
            {
                return NotFound();
            }
            //Ticket tickets = new Ticket
            //{
            //    numberOfTickets = numberOfTickets,
            //    seatNumber = seatNumber
            //};
            //_ticketService.BookTicket(movieName, theaterName, tickets);

            //return Ok("Ticket booked successfully!");
        }
        [HttpGet]
        public IActionResult GetBookedTicketsCount(string movieName, string theatreName)
        {
            try
            {
                int bookedTicketsCount = _ticketService.GetBookedTicketsCount(movieName, theatreName);
                return Ok(bookedTicketsCount);
            }
            catch (Exception ex)
            {
                // Handle any exceptions or validation errors
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }


    }


}
