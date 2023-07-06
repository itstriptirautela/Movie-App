using Movie_App.Model;

namespace Movie_App.Service
{
    public interface ITicketService
    {
        //public void BookTicket(Ticket ticket);
        public void BookTicket(string movieName, string theaterName, Ticket tickets);
        // public void BookTicket(string movieName, string theaterName, int numberOfTickets, List<string> seatNumber);
        public int GetBookedTicketsCount(string movieName, string theatreName);
        public void UpdateBookedTickets(string movieName, string theatreName, int bookedTicketsCount);
        public void UpdateTicketStatus(string movieName, string theatreName,string ticketStatus);
    }
}
