using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyTicketsClient.Models
{
    public class MyTicket
    {
        public int TicketId { get; set; }
        public string Username { get; set; } = null!;
        public string? TeamName { get; set; } = null!;
        public int Gate { get; set; }
        public int? Row { get; set; }
        public int? Seats { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public bool IsAccepted { get; set; } = false;
        public string? BuyerUsername { get; set; } = null!;
        public string? BuyerPhone { get; set; } = null!;
        public string AwayTeam { get; set; } = null!;
        public string HomeTeam { get; set; } = null!;
    }
}
