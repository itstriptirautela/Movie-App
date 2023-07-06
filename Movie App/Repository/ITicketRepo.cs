using Movie_App.Model;

namespace Movie_App.Repository
{
    public interface ITicketRepo
    {
        // public void BookTicket(Ticket ticket);
        public void BookTicket(Ticket ticket, string movieName, string TheaterName);
        public int GetBookedTicketsCount(string movieName, string theatreName);
        public void UpdateBookedTickets(string movieName, string theatreName, int bookedTicketsCount);
        public void UpdateTicketStatus(string movieName, string theatreName, string ticketStatus);
    }
}
