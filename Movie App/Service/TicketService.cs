using Movie_App.Model;
using Movie_App.Repository;

namespace Movie_App.Service
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepo _ticketRepository;

        public TicketService(ITicketRepo ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

       
        public void BookTicket(string movieName, string theaterName, Ticket tickets)
        {
            try
            {
          

                if (tickets.numberOfTickets != tickets.seatNumber.Count)
                {
                   
                    throw new Exception("The number of tickets provided does not match the number of seat numbers provided.");
                }

                Ticket ticket = new Ticket
                {
                    numberOfTickets = tickets.numberOfTickets,
                    seatNumber = tickets.seatNumber
                };

                _ticketRepository.BookTicket(ticket, movieName, theaterName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to book tickets:{ex.Message}");
            }   
        }
        public int GetBookedTicketsCount(string movieName, string theatreName)
        {
            return _ticketRepository.GetBookedTicketsCount(movieName, theatreName);
        }

        public void UpdateBookedTickets(string movieName, string theatreName, int bookedTicketsCount)
        {
            _ticketRepository.UpdateBookedTickets(movieName, theatreName, bookedTicketsCount);
        }
        public void UpdateTicketStatus(string movieName, string theatreName, string ticketStatus)
        {
            _ticketRepository.UpdateTicketStatus(movieName, theatreName,ticketStatus);
        }
    }


}
